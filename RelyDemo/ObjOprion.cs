using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelyDemo
{
   public class ObjOprion
    {
        private readonly ObjConfiguration oc;
        public ObjOprion(ObjConfiguration obj) {
            oc = obj;
        }

        public void Show() {
            Console.WriteLine(oc.name);
        }
    }
}
