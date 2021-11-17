using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace RelyDemo
{
   public class RedisDb
    {
        public IDatabase db { get {
                return _db;
            } }
        private IDatabase _db;
        private readonly ILogger<RedisDb> logger;
        private ConnectionMultiplexer _redis;
        public RedisDb(ILogger<RedisDb> _logger) {
            logger = _logger;
            ConfigurationOptions options = new ConfigurationOptions()
            {
                DefaultDatabase=1,
                ClientName= "wen",
                Password="123456789",
                AbortOnConnectFail=false,
                ConnectRetry=4,
                ConnectTimeout=3,
                EndPoints =
                {
                  { "127.0.0.1", 6379 }
                },
            };
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(options);
            _redis = redis;
            if (!redis.IsConnected)
            {
                logger.LogWarning("redis未连接");
                return;
            }
            _db = redis.GetDatabase();
            logger.LogInformation("redis连接成功");
        }

        public ISubscriber Subscribe() {
            return _redis.GetSubscriber();
        }
    }
}
