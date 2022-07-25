using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSanFelipe.Entity.Response
{
    public class ClientesVentasResponse
    {
        public int idcliente { get; set; }
        public int idzonaUbigeo { get; set; }
        public string nombreCliente {get;set;}
        public DateTime    fechaCreacion { get; set; }
        public bool    estado { get; set; }
    }
}
