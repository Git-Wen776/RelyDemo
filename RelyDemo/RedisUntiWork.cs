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

        public Task<RedisValue> strGet(string key) {
            return db.StringGetAsync(key);
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

        public async Task<T> HashGet<T>(string key,string filed)
        {
            if (! await db.HashExistsAsync(key, filed))
                return default;
            string value = await db.HashGetAsync(key, filed);
            return _serializeHelper.DesSerialize<T>(value);
        }

        public async Task<List<T>> HashGetList<T>(string key)
        {
            if(! await db.KeyExistsAsync(key))
                return new List<T> { default };
            List<T> entitys = new();
            foreach(var value in await db.HashValuesAsync(key))
            {
                entitys.Add(_serializeHelper.DesSerialize<T>(value));
            }
            return entitys;
        }

        //public async Task<bool> Lpush(string key,string filed,)
        //{
        //    if (await db.KeyExistsAsync(key))
        //        return false;
        //    return await db.ListLeftPushAsync();
        //}
        
        
    }
}
