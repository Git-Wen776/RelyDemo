using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.Logging.Console;
using Newtonsoft.Json;

namespace RelyDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IServiceCollection service = new ServiceCollection();
            service.AddLogging(
                builder => {
                    //builder.AddConsole();
                    builder.SetMinimumLevel(LogLevel.Trace);
                    builder.AddAWLogger();
                }
                );
            service.AddScoped<IReadTxt, ReadTxt>();
            service.AddScoped<ILog, Logger>();
            service.AddSingleton<AWConfigraution>();
            service.AddScoped<IFileOp, FileOp>();
            service.AddObjService(p=>{
                p.name ="马东西";
            });
            service.AddSingleton<RedisDb>();
            service.AddScoped<IRedisUntiWrok,RedisUntiWork>();
            service.AddSingleton<SerializeHelper>(p => SerializeHelper.GetSerialize());
            using (ServiceProvider sp = service.BuildServiceProvider()) {
                ILogger logger = sp.GetService<ILogger<Program>>();
                logger.LogInformation("程序正常运行");
                logger.LogInformation(AppDomain.CurrentDomain.BaseDirectory);
                //var read = sp.GetService<IReadTxt>();
                //await read.ConsoleReadAsync();
                //var obb = sp.GetService<ObjOprion>();
                //obb.Show();
                var rediswork = sp.GetService<IRedisUntiWrok>();
                //await rediswork.strSet("person", "马");
                await Task.Delay(10);
                //await rediswork.HashSet("stu", "xiaochen", new Student
                //{
                //    Classidd="1",
                //    Id="2",
                //    Name="xiao陈",
                //    downurl=""
                //});
                var value =await rediswork.HashGet<Student>("stu", "xiaowan");
                Console.WriteLine($"我在hash里面获取xiaowan--{value.Name}");
                var p = rediswork.strGet("person");
                var list = await rediswork.HashGetList<Student>("stu");
                foreach(var item in list)
                {
                    Console.WriteLine(item.Name);
                }
                logger.LogInformation($"从redis获取的数据是{await p}");
                Console.ReadKey();
            }
        }
    }
    public class Student
    {
        public string Classidd { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string downurl { get; set; }
    }

    public static class ObjExtexsions {
        public static IServiceCollection AddObjService(this IServiceCollection services, Action<ObjConfiguration> ob) {
            var obj = new ObjConfiguration();
            ob.Invoke(obj);
            services.AddScoped<ObjOprion>(p=>new ObjOprion(obj));
            return services;
        }
    }
}
