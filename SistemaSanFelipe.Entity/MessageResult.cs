using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSanFelipe.Entity
{
    [DataContract]
    public sealed class MessageResult
    {
        [DataMember(Name = "message")]
        public string Message { get; private set; }

        private MessageResult(string message)
        {
            Message = message;
        }

        public static MessageResult Of(string message)
        {
            var result = new MessageResult(message);

            return result;
        }
    }
}
