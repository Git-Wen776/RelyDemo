using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.Logging.Console;

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
            using (ServiceProvider sp = service.BuildServiceProvider()) {
                ILogger logger = sp.GetService<ILogger<Program>>();
                logger.LogInformation("程序正常运行");
                logger.LogInformation(AppDomain.CurrentDomain.BaseDirectory);
                //var read = sp.GetService<IReadTxt>();
                //await read.ConsoleReadAsync();
                //var obb = sp.GetService<ObjOprion>();
                //obb.Show();
                //var rediswork = sp.GetService<IRedisUntiWrok>();
                //await  rediswork.strSet("person", "马");
                //logger.LogInformation($"从redis获取的数据是{rediswork.strGet("person")}");
                var rediswork = sp.GetService<IRedisUntiWrok>();
                if (Console.ReadLine() == "1")
                {
                    while (true)
                    {
                        Console.WriteLine("请输入要发布的管道");
                        var channles = Console.ReadLine();
                        Console.WriteLine("请输入要发布的信息");
                        var message = Console.ReadLine();
                        await rediswork.PublishAsync(channles, message);
                    }
                }
                else {
                    Console.WriteLine("请输入要订阅的管道");
                    var cha = Console.ReadLine();
                    var sub= rediswork.SubScribeAsync(cha);
                    Console.WriteLine($"订阅的管道是{cha}");
                    await sub;
                    Console.ReadKey();
                }
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
