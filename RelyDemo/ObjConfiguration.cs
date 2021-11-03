using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelyDemo
{
    public class ObjConfiguration
    {
        public string name { get; set; }
        public DateTime time = DateTime.Now;
        public string filepath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
    }
}
