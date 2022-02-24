using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSanFelipe.Entity.Request
{
   public  class TokenRequest
    {
        public string TokenCode { get; set; }
        public int TokenType { get; set; }
    }
}
