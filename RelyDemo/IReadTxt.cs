using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelyDemo
{
   public interface IReadTxt
    {
        Task<List<Student>> ConsoleReadAsync();
    }
}
