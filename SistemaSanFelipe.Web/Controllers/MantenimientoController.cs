using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rotativa.AspNetCore;
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
    public class MantenimientoController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger Logger;
        private readonly IHostingEnvironment HostingEnvironment;
        public MantenimientoController(IConfiguration IConfiguration, ILoggerFactory LoggerFactory, IHostingEnvironment IHostingEnvironment)
        {
            HostingEnvironment = IHostingEnvironment;
            Configuration = IConfiguration;
            Logger = LoggerFactory.CreateLogger<MainController>();
            GeneralModel.UrlWebApi = Configuration["Urls:UrlWebApi"];
        }


        //[HttpGet]
        //public IActionResult GetNumeroGuiaVenta(int numeroGuia)
        //{
        //    var Response = new GenericObjectResponse();
        //    try
        //    {
        //        var Url = GeneralModel.UrlWebApi + "Venta/GetNumeroGuiaVenta/" + numeroGuia;
        //        var Result = Utilities.Rest.RestClient.ProcessGetRequest(Url);
        //        Response.data = Result;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.LogWarning(ex, ex.Message);
        //        Response.message = ex.Message;
        //    }
        //    return Json(Response);
        //}
 
        public IActionResult GetNumeroGuiaVenta()
        {
            var Response = new GenericObjectResponse();
            try
            {
                var token = HttpContext.Session.GetString("TOKEN");
                var UrlUser = GeneralModel.UrlWebApi + "Venta/GetNumeroGuiaVenta" ;
                var ResultUser = SistemaSanFelipe.Utilities.Rest.RestClient.ProcessGetRequest(UrlUser, token);
                var ResultUserJson = JsonConvert.SerializeObject(ResultUser);
                var UserData = JsonConvert.DeserializeObject<NumeroVentaResponse>(ResultUserJson);
                Response.data = UserData;
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex, ex.Message);
                Response.message = ex.Message;
            }
            return Ok(Response);
        }


        [HttpGet]
        public IActionResult GetBandejaCliente()
        {
            var Response = new GenericObjectResponse();
            try
            {
                // HttpContext.Session.SetInt32("PROFILE_ID", UserData.ProfileId);
                var PerfilId = HttpContext.Session.GetInt32("PROFILE_ID");
                var UsuarioCreacion = HttpContext.Session.GetInt32("USUARIO_ID");

                //if (HttpContext.Session.GetInt32("TOKEN") == null || HttpContext.Session.GetString("TOKEN") == null)
                //{
                //    Response.code = (int)Enums.eCodeError.SESIONCADUCA;
                //    Response.message = "Su sesión ha caducado.";
                //    Response.data = "SESSION_TIMEOUT";
                //    return Json(Response);
                //}
                 
                var Url = GeneralModel.UrlWebApi + "Venta/GetClientesVentas";
                var Result = SistemaSanFelipe.Utilities.Rest.RestClient.ProcessGetRequest(Url);
                Response.data = Result;

            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex, ex.Message);
                Response.message = ex.Message;
            }
            return Json(Response);
        }


        [HttpGet]
        public IActionResult GetClienteId(int idCliente)
        {
            var Response = new GenericObjectResponse();
            try
            {
                // HttpContext.Session.SetInt32("PROFILE_ID", UserData.ProfileId);
                var PerfilId = HttpContext.Session.GetInt32("PROFILE_ID");
                var UsuarioCreacion = HttpContext.Session.GetInt32("USUARIO_ID");
                //if (HttpContext.Session.GetInt32("TOKEN") == null || HttpContext.Session.GetString("TOKEN") == null)
                //{
                //    Response.code = (int)Enums.eCodeError.SESIONCADUCA;
                //    Response.message = "Su sesión ha caducado.";
                //    Response.data = "SESSION_TIMEOUT";
                //    return Json(Response);
                //} 
                var Url = GeneralModel.UrlWebApi + "Venta/GetClienteId/" + idCliente;
                var Result = SistemaSanFelipe.Utilities.Rest.RestClient.ProcessGetRequest(Url);
                Response.data = Result;
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex, ex.Message);
                Response.message = ex.Message;
            }
            return Json(Response);
        }
         
        public IActionResult ObtenerListadoPollos()
        {
            var Response = new GenericObjectResponse();
            try
            {
                // HttpContext.Session.SetInt32("PROFILE_ID", UserData.ProfileId);
                var PerfilId = HttpContext.Session.GetInt32("PROFILE_ID");
                var UsuarioCreacion = HttpContext.Session.GetInt32("USUARIO_ID");

                //if (HttpContext.Session.GetInt32("TOKEN") == null || HttpContext.Session.GetString("TOKEN") == null)
                //{
                //    Response.code = (int)Enums.eCodeError.SESIONCADUCA;
                //    Response.message = "Su sesión ha caducado.";
                //    Response.data = "SESSION_TIMEOUT";
                //    return Json(Response);
                //}

                var Url = GeneralModel.UrlWebApi + "Venta/ObtenerListadoPollos";
                var Result = SistemaSanFelipe.Utilities.Rest.RestClient.ProcessGetRequest(Url);
                Response.data = Result;

            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex, ex.Message);
                Response.message = ex.Message;
            }
            return Json(Response);
        }

         
        public IActionResult GuardarGuiaVenta(GuiaVentaPolloRequest request)
        {
            var Response = new GenericObjectResponse();
            try
            {
                //if (request.FechaCorte == null)
                //{
                //    DateTime fecha = DateTime.Now;
                //    request.FechaCorte = Convert.ToString(fecha);
                //}
                var IdUsuario = HttpContext.Session.GetInt32("USUARIO_ID");
                request.UsuarioId = Convert.ToInt32(IdUsuario);

                var Url = GeneralModel.UrlWebApi + "Venta/GuardarGuiaVenta";
                var Result = SistemaSanFelipe.Utilities.Rest.RestClient.ProcessPostRequest(Url, request);     
                var resultado = Convert.ToInt32(Result);

                if (resultado == 1)
                {
                    Response.code = (int)Enums.eCodeError.OK;
                    Response.data = Result;
                    return Json(Response);
                }
                else
                {
                    Response.code = (int)Enums.eCodeError.VAL;
                    Response.data = Result;
                    return Json(Response);
                }

            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex, ex.Message);
                Response.message = ex.Message;
                return Json(Response);
            }
        }


        public IActionResult DescargarPdf()
        { 
            //TotalVenta TotalVenta = new TotalVenta();  

            var Url = GeneralModel.UrlWebApi + "Venta/GetVentasReporteLista";
            var Result = SistemaSanFelipe.Utilities.Rest.RestClient.ProcessGetRequest(Url);
            var ResultUserJson = JsonConvert.SerializeObject(Result);
            var UserData = JsonConvert.DeserializeObject<VentasReporteLista>(ResultUserJson);
               
            return new ViewAsPdf("DescargarPdf", UserData)
            {

            };
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
