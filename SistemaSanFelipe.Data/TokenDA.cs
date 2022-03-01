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
   public  class TokenDA : IDisposable
    {
        private string ConStr;
        public TokenDA(string ConStr)
        {
            this.ConStr = ConStr;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public UsuarioResponse GetUserByToken(TokenRequest Token)
        {
            UsuarioResponse Entity = null;

            using (var Ado = new SQLServer(ConStr))
            {
                var Parameters = new SqlParameter[]
                {
                    new SqlParameter{ParameterName="@TokenCode",SqlDbType=SqlDbType.VarChar,Size=50,SqlValue=Token.TokenCode},
                    new SqlParameter{ParameterName="@TokenType",SqlDbType=SqlDbType.Int,SqlValue=Token.TokenType}
                };
                var Dr = Ado.ExecDataReader("usp_GetUserByToken @TokenCode,@TokenType ", Parameters);

                if (!Dr.HasRows) return Entity;
                while (Dr.Read())
                {
                    Entity = new UsuarioResponse();
                    Entity.UserCode = Convert.ToString(Dr["UsuarioCodeGuid"]);
                    Entity.Login = (string)Dr["Login"];
                    Entity.Fullname = (string)Dr["NombreCompleto"];
                    Entity.DocumentType = Convert.ToInt32(Dr["TipoDocumento"]);
                    Entity.DocumentNumber = (string)Dr["DocumentoIdentidad"];
                    Entity.Email = (string)Dr["Email"];
                    Entity.ProfileId = (int)Dr["IdPerfil"];
                    Entity.Status = Convert.ToInt32(Dr["Estado"]);
                    Entity.IdUsuario = (int)Dr["IdUsuario"];
                    Entity.Sexo = (string)Dr["sexo"]; ;

                    break;
                }
                Dr.Close();
            }
            return Entity;
        }
    }
}
