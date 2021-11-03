using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelyDemo
{
   public class ReadTxt:IReadTxt
    {
        private readonly string filepath = @"E:\vs 2019\Demo\ConsoleApp1";

        private readonly ILogger<ReadTxt> log;
        public ReadTxt(ILogger<ReadTxt> _log)
        {
            log = _log;
        }
        public async Task<List<Student>> ConsoleReadAsync()
        {
            DirectoryInfo info = new DirectoryInfo(filepath);
            DateTime dt = DateTime.Now;
            if (!info.Exists)
            {
                log.LogWarning("文件夹不存在");
                return null;
            }
            string[] suffix = null;
            string linetainer = string.Empty;
            FileInfo[] files = info.GetFiles();
            List<Student> students = new List<Student>();
            foreach (var item in files)
            {
                suffix = item.Name.Split('.');
                if (suffix[suffix.Length - 1] != "txt")
                    continue;
                FileStream fs = new FileStream(item.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);
                StreamReader sr = new StreamReader(fs);
                string line = string.Empty;
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    suffix = line.Split(',');
                    Student stu = new Student()
                    {
                        Classidd = suffix[0],
                        Id = suffix[1],
                        Name = suffix[2],
                        downurl = suffix[3]
                    };
                    students.Add(stu);
                }
                log.LogWarning($"正在读入文件{item.Name}");
                sr.Close();
                fs.Close();
            }
            var timespan = (DateTime.Now - dt).TotalMilliseconds;
            log.LogDebug($"共读入:{students.Count}条记录,共耗时{timespan}毫秒");
            return students;
        }
    }
}
