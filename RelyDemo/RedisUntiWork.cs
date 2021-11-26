using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace RelyDemo
{
  public class RedisUntiWork:IRedisUntiWrok
    {
        private readonly IDatabase db;
        private readonly RedisDb redisDb;
        private readonly ISubscriber sub;
        private readonly ILogger<RedisUntiWork> _logger;
        private readonly SerializeHelper _serializeHelper;

        public RedisUntiWork(RedisDb _redis,ILogger<RedisUntiWork> logger,SerializeHelper serializeHelper) {
            redisDb = _redis;
            db = redisDb.db;
            sub = redisDb.Subscribe();
            _logger = logger;
            _serializeHelper = serializeHelper;
        }

        public List<T> getList<T>(string key)
        {
            throw new NotImplementedException();
        }

        public void ListRemove<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public Task PublishAsync(string channels, string message)
        {
            return sub.PublishAsync(channels,message);
        }

        public string strGet(string key) {
            return db.StringGet(key);
        }

        public Task strSet(string key,string value)
        {
            return db.StringSetAsync(key,value);
        }

        public  Task SubScribeAsync(string channels)
        {
            return sub.SubscribeAsync(channels, (channels, message) =>
            {
                //_logger.LogInformation($"订阅的管道是{channels},发布的消息是{message}");
                Console.WriteLine($"订阅的管道是{channels},发布的消息是{message}");
            });
        }

        public async Task<bool> StringSetList<T>(string key, List<T> entitys)
        {
            if (entitys == null)
                throw new ArgumentNullException("参数为空");
            if (await db.KeyExistsAsync(key))
                return false;
            var valuestr=_serializeHelper.Serialize(entitys);
            return await db.StringSetAsync(key, valuestr);
        }

        public async Task<bool> HashSet<T>(string key,string filed,T t)
        {
            if(t== null)
                throw new ArgumentNullException("参数为空");
            if (await db.HashExistsAsync(key,filed))
                return false;
            var str=_serializeHelper.Serialize(t);
            return await db.HashSetAsync(key, filed, str);
        }
    }
}
