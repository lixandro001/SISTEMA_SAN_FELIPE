using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSanFelipe.Entity
{
   public static  class Enums
    {

        public enum eCodeError : int
        {
            OK = 0,
            ERROR = 1,
            VAL = 2
        }

        public enum TokenType : int
        {
            LOGIN = 1,
            PASSWORD = 2
        }

        public enum Profile : int
        {
            ADMINISTRADOR = 1,
            ASESORCOMERCIAL = 2,
            ANALISTACREDITO = 3,
            GERENTENEGOCIO = 4
        }
    }
}
