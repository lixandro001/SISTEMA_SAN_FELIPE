using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSanFelipe.Entity
{
   public  class ErrorHandler : Exception
    {
        [IgnoreDataMember]
        public HttpStatusCode Code { get; }

        [DataMember(Name = "message")]
        public string Message { get; }

        public ErrorHandler(HttpStatusCode code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
