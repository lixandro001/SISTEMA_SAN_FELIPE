using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SistemaSanFelipe.Business;
using SistemaSanFelipe.Entity;
using SistemaSanFelipe.Entity.Request;
using SistemaSanFelipe.Entity.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSanFelipe.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeguridadController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger Logger;
        private SeguridadBL objSeguridadBL;
        public SeguridadController(IConfiguration IConfiguration, ILoggerFactory LoggerFactory)
        {
            Configuration = IConfiguration;
            Logger = LoggerFactory.CreateLogger<SeguridadController>();
            objSeguridadBL = new SeguridadBL(Configuration["ConnectionStrings:SIMED"]);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginRequest model)
        {
            try
            {
                Logger.LogWarning("Ingreso");

                GenericResponse response = new GenericResponse();
                SeguridadResponse Security = objSeguridadBL.GetLogin(model);

                if (Security != null)
                {
                    if (Security.Locked)
                    {
                        response.code = (int)Enums.eCodeError.VAL;
                        response.message = "El Usuario Se Encuentra Bloqueado";
                    }
                    else
                    {
                        response.code = (int)Enums.eCodeError.OK;
                        response.message = "Acceso Correcto";
                        response.data = Security.TokenCode;
                    }
                }
                else
                {
                    response.code = (int)Enums.eCodeError.ERROR;
                    response.message = "Usuario o clave Incorrectos";
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex.Message);
                Logger.LogError(ex.Message);
                throw ex;
            }
        }


    }
}
