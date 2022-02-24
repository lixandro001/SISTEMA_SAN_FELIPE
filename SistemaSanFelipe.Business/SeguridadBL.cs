using SistemaSanFelipe.Data;
using SistemaSanFelipe.Entity;
using SistemaSanFelipe.Entity.Request;
using SistemaSanFelipe.Entity.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSanFelipe.Business
{
    public class SeguridadBL : IDisposable
    {
        private SeguridadDA objSeguridadDL;

        public SeguridadBL(string ConStr)
        {
            objSeguridadDL = new SeguridadDA(ConStr);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public SeguridadResponse GetLogin(LoginRequest Login)
        {
            SeguridadResponse Security = objSeguridadDL.GetLogin(Login);

            if (Security != null)
            {
                if (!Security.Locked)
                {
                    TokenResponse Token = objSeguridadDL.GetNewToken(Security.UsuarioCodeGuid, (int)Enums.TokenType.LOGIN);
                    Security.TokenCode = Token.TokenCode;

                }
            }

            return Security;
        }



    }
}
