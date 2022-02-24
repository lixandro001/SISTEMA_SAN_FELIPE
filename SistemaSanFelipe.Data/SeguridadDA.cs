using SistemaSanFelipe.Entity.Request;
using SistemaSanFelipe.Entity.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSanFelipe.Data
{
   public class SeguridadDA
    {
        private string ConStr;
        public SeguridadDA(string ConStr)
        {
            this.ConStr = ConStr;
        }
        public void Disponse()
        {
            GC.SuppressFinalize(this);

        }

        public SeguridadResponse GetLogin(LoginRequest Login)
        {
            var Entity = new SeguridadResponse();
            using (var Ado = new SQLServer(ConStr))
            {
                var Parameters = new SqlParameter[]
                {
                    new SqlParameter{ParameterName="@Login",SqlDbType=SqlDbType.VarChar,Size=50,SqlValue=Login.Login},
                    new SqlParameter{ParameterName="@Password",SqlDbType=SqlDbType.VarChar,Size=50,SqlValue=Login.Password}
                };

                var Dr = Ado.ExecDataReader("usp_login @Login, @Password", Parameters);

                if (!Dr.HasRows) return null;
                while (Dr.Read())
                {
                    Entity.IdPerfil = Convert.ToInt32(Dr["IdPerfil"]);
                    Entity.UsuarioCodeGuid = Convert.ToString(Dr["UsuarioCodeGuid"]);
                    Entity.Login = Convert.ToString(Dr["Login"]);
                    Entity.Password = Convert.ToString(Dr["Password"]);
                    Entity.NombreCompleto = Convert.ToString(Dr["NombreCompleto"]);
                    Entity.TipoDocumento = Convert.ToInt32(Dr["TipoDocumento"]);
                    Entity.DocumentoIdentidad = Convert.ToString(Dr["DocumentoIdentidad"]);
                    Entity.Email = Convert.ToString(Dr["Email"]);
                    Entity.Estado = Convert.ToBoolean(Dr["Estado"]);
                    Entity.Locked = (bool)Dr["Locked"];
                    break;
                }
                Dr.Close();
            }
            return Entity;
        }

        public TokenResponse GetNewToken(string UserCode, int Tokentype)
        {
            var Entity = new TokenResponse();
            using (var Ado = new SQLServer(ConStr))
            {
                var Parameters = new SqlParameter[]
                {
                    new SqlParameter{ParameterName="@UsuarioCodeGuid",SqlDbType=SqlDbType.VarChar,Size=50,SqlValue=UserCode},
                    new SqlParameter{ParameterName="@TokenType",SqlDbType=SqlDbType.Int,SqlValue=Tokentype}
                };

                var Dr = Ado.ExecDataReader("usp_GetNewToken @UsuarioCodeGuid,@TokenType", Parameters);

                if (!Dr.HasRows) return null;

                while (Dr.Read())
                {
                    Entity.TokenCode = Convert.ToString(Dr["TokenCode"]);
                    break;
                }
                Dr.Close();
            }
            return Entity;
        }
    }
}
