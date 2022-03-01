using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSanFelipe.Entity.Response
{
   public  class GenericResponse
    {
        public int code { get; set; }
        public string message { get; set; }
        public string data { get; set; }
        public object Datas { get; set; }

    }
}
