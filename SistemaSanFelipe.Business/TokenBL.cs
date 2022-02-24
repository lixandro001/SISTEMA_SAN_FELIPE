using SistemaSanFelipe.Data;
using SistemaSanFelipe.Entity.Request;
using SistemaSanFelipe.Entity.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSanFelipe.Business
{
   public  class TokenBL
    {
        private TokenDA objTokenDL;

        public TokenBL(string ConStr)
        {
            objTokenDL = new TokenDA(ConStr);
        }
        public void Disponse()
        {
            GC.SuppressFinalize(this);
        }
        public UsuarioResponse GetUserByToken(TokenRequest Token)
        {
            return objTokenDL.GetUserByToken(Token);
        }
    }
}
