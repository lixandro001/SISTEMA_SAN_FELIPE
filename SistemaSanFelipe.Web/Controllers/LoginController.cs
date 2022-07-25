using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SistemaSanFelipe.Entity;
using SistemaSanFelipe.Entity.Request;
using SistemaSanFelipe.Entity.Response;
using SistemaSanFelipe.Utilities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaSanFelipe.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly IHostingEnvironment HostingEnvironment;
        private readonly ILogger Logger;
        //private readonly IServiceCollection ServiceCollection;

        public LoginController(IConfiguration IConfiguration, ILoggerFactory LoggerFactory, IHostingEnvironment IHostingEnvironment/*, IServiceCollection services*/)
        {
            HostingEnvironment = IHostingEnvironment;
            Configuration = IConfiguration;
            Logger = LoggerFactory.CreateLogger<LoginController>();
            GeneralModel.UrlWebApi = Configuration["Urls:UrlWebApi"];
            //ServiceCollection = services;
            //ServiceCollection.AddSession();
            //ServiceCollection.AddMvc();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Validate(LoginRequest Request)
        {
            var Response = new GenericObjectResponse();
            try
            {
                var Url = GeneralModel.UrlWebApi + "seguridad/Login";
                var Result = SistemaSanFelipe.Utilities.Rest.RestClient.ProcessPostRequest(Url, Request);

                Response.data = Result;
                  
                var ResultJson = JsonConvert.SerializeObject(Response.data);
                var User = JsonConvert.DeserializeObject<GenericResponse>(ResultJson);

                switch (User.code)
                {
                    case (int)Enums.eCodeError.OK:
                        var UrlUser = GeneralModel.UrlWebApi + "usuario/GetUserDataByToken";
                        var ResultUser =  SistemaSanFelipe.Utilities.Rest.RestClient.ProcessGetRequest(UrlUser, User.data);
                        var ResultUserJson = JsonConvert.SerializeObject(ResultUser);
                        var UserData = JsonConvert.DeserializeObject<UserResponse>(ResultUserJson);

                        HttpContext.Session.SetString("TOKEN", User.data);
                        HttpContext.Session.SetInt32("PROFILE_ID", UserData.ProfileId);
                        HttpContext.Session.SetInt32("USUARIO_ID", UserData.IdUsuario);
                        HttpContext.Session.SetString("SEXO_ID", UserData.Sexo);

                        break;
                    default:
                        Response.message = User.message;
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex, ex.Message);
                Response.message = ex.Message;
            }
            return  Json(Response);
        }
    }
}
