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
using System.Globalization;
using System.IO;
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
         
        public IActionResult ObtenerListadoPollos(string StartDate, string EndDate)
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
                if (!string.IsNullOrEmpty(StartDate))
                {
                    DateTime CDate1 = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    StartDate = string.Format("{0:yyyy-MM-dd}", CDate1);
                }

                if (!string.IsNullOrEmpty(EndDate))
                {
                    DateTime CDate2 = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    EndDate = string.Format("{0:yyyy-MM-dd}", CDate2);
                }

                var Url = GeneralModel.UrlWebApi + "Venta/ObtenerListadoPollos/" + StartDate + "/" + EndDate;
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

        public IActionResult ExportExcel(string StartDate, string EndDate)
        {

            try
            {
                string FileName = string.Format("facturacion_data_{0:ddMMyyyy_hhmm}.xlsx", DateTime.Now);

                string PathTemplate = Path.Combine(HostingEnvironment.WebRootPath, "Template/DescargarPdfPollos.xlsx");
                string NewPath = Path.Combine(HostingEnvironment.WebRootPath, "temporal/" + FileName);
    
                if (!string.IsNullOrEmpty(StartDate))
                {
                    DateTime CDate1 = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    StartDate = string.Format("{0:yyyy-MM-dd}", CDate1);
                }

                if (!string.IsNullOrEmpty(EndDate))
                {
                    DateTime CDate2 = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    EndDate = string.Format("{0:yyyy-MM-dd}", CDate2);
                }

                using (var pck = new OfficeOpenXml.ExcelPackage())
                {
                    using (var stream = System.IO.File.OpenRead(PathTemplate))
                    {
                        pck.Load(stream);
                    }
                     
                    var Url = GeneralModel.UrlWebApi + "Venta/ObtenerListadoPollos/" + StartDate + "/" + EndDate;
                    
                    var ResultUser = SistemaSanFelipe.Utilities.Rest.RestClient.ProcessGetRequest(Url);
                    var ResultUserJson = JsonConvert.SerializeObject(ResultUser);
                    var Response = JsonConvert.DeserializeObject<List<ListadoPollosResponse>>(ResultUserJson);
                     
                    var ws = pck.Workbook.Worksheets["Data"];

                    int Position = 2;
                    foreach (var itm in Response)
                    {
                        ws.Cells[Position, 1].Value = "GSA";
                        ws.Cells[Position, 2].Value = itm.codigoventa;
                        ws.Cells[Position, 3].Value = itm.idventa;
                        ws.Cells[Position, 4].Value = itm.CfechaSalidaVenta;
                        ws.Cells[Position, 5].Value = itm.nombreUbigeo;
                        ws.Cells[Position, 6].Value = itm.nombreCliente;
                        ws.Cells[Position, 7].Value = itm.JABAS;
                        ws.Cells[Position, 8].Value = itm.AVES;
                        ws.Cells[Position, 9].Value = itm.pesoBRUTO;
                        ws.Cells[Position, 10].Value = itm.TARA;
                        ws.Cells[Position, 11].Value = itm.PESONETOG;
                        ws.Cells[Position, 12].Value = itm.PesoPromedio;
                        ws.Cells[Position, 13].Value = itm.UNITARIO;
                        ws.Cells[Position, 14].Value = itm.Total;
                        ws.Cells[Position, 15].Value = itm.porCobrar;
                         
                        Position++;
                    }

                    pck.SaveAs(new FileInfo(NewPath));
                    byte[] FileBytes = System.IO.File.ReadAllBytes(NewPath);
                    //FileBytes = pck.GetAsByteArray();
                    return File(FileBytes, "application/force-download", "DataReportePollos.xlsx");
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex, ex.Message);
                return Content("No hay registros");
            }
        }


        public IActionResult DescargarPdf(string FechaInicio, string FechaFinal)
        {

            try
            {

                if (!string.IsNullOrEmpty(FechaInicio))
                {
                    DateTime CDate1 = DateTime.ParseExact(FechaInicio, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    FechaInicio = string.Format("{0:yyyy-MM-dd}", CDate1);
                }
                if (!string.IsNullOrEmpty(FechaFinal))
                {
                    DateTime CDate2 = DateTime.ParseExact(FechaFinal, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    FechaFinal = string.Format("{0:yyyy-MM-dd}", CDate2);
                }
                var Url = GeneralModel.UrlWebApi + "Venta/GetVentasReporteLista/" + FechaInicio + "/" + FechaFinal;
                var Result = SistemaSanFelipe.Utilities.Rest.RestClient.ProcessGetRequest(Url);
                var ResultUserJson = JsonConvert.SerializeObject(Result);
                var UserData = JsonConvert.DeserializeObject<VentasReporteLista>(ResultUserJson);

                return new ViewAsPdf("DescargarPdf", UserData)
                {

                };
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex, ex.Message);
                return Content("No hay registros");
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
