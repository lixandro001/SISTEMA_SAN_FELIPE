using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSanFelipe.Entity.Response
{
   public  class UserResponse
    {
        public string UserCode { get; set; }
        public string Login { get; set; }
        public string Fullname { get; set; }
        public int DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string Email { get; set; }
        public int ProfileId { get; set; }
        public int Status { get; set; }
        public int IdUsuario { get; set; }
        public string Sexo { get; set; }
    }
}
