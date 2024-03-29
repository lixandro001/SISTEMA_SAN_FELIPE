﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SistemaSanFelipe.Business;
using SistemaSanFelipe.Entity;
using SistemaSanFelipe.Entity.Request;
using SistemaSanFelipe.Entity.Response;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;


namespace SistemaSanFelipe.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger Logger;
        private SeguridadBL objSecurityBL;
        private TokenBL objTokenBL;
        private ventaBL objventaBL;

        public VentaController(IConfiguration IConfiguration, ILoggerFactory LoggerFactory)
        {
            Configuration = IConfiguration;
            Logger = LoggerFactory.CreateLogger<UsuarioController>();
            objSecurityBL = new SeguridadBL(Configuration["ConnectionStrings:SIMED"]);
            objTokenBL = new TokenBL(Configuration["ConnectionStrings:SIMED"]);
            objventaBL=new ventaBL(Configuration["ConnectionStrings:SIMED"]);

        }
        [HttpGet]
        [Route("GetNumeroGuiaVenta")] 
        public IActionResult GetNumeroGuiaVenta()
        {
            try
            {
                //Obtener el Token
                string secretTokenName = "Auth-Token";
                var tokenCode = HttpContext.Request.Headers[secretTokenName].ToString();
      
                //Request.TokenCode = tokenCode;
                //Request.TokenType = (int)Enums.TokenType.LOGIN;
                var Result = objventaBL.GetNumeroGuiaVenta();
                return Ok(Result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpGet]
        [Route("GetClienteId/{idCliente?}")]
        public IActionResult GetClienteId(int idCliente)
        {
            try  
            {
                //Obtener el Token
                string secretTokenName = "Auth-Token";
                var tokenCode = HttpContext.Request.Headers[secretTokenName].ToString();
                ClientesVentasResponse Request = new ClientesVentasResponse();
                Request.idcliente = idCliente;
                //Request.TokenCode = tokenCode;
                //Request.TokenType = (int)Enums.TokenType.LOGIN;

                var Result = objventaBL.GetClienteId(idCliente);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                throw;
            }
        }
         
        [HttpGet]
        [Route("GetClientesVentas")]
        public IActionResult  GetClientesVentas()
        {
            try
            {
                //Obtener el Token
                string secretTokenName = "Auth-Token";
                var tokenCode = HttpContext.Request.Headers[secretTokenName].ToString();    
                var Result = objventaBL.GetClientesVentas();
                //response.code = (int)Enums.eCodeError.OK;
                //response.Data = data;
                return Ok(Result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpGet]
        [Route("GetVentasReporteLista/{FechaInicio?}/{FechaFinal?}")] 
        public IActionResult GetVentasReporteLista(string FechaInicio, string FechaFinal)
        {
            try
            {
                //Obtener el Token
                string secretTokenName = "Auth-Token";
                var tokenCode = HttpContext.Request.Headers[secretTokenName].ToString();
                DateTime fechaIniDate = DateTime.ParseExact(FechaInicio, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime fechaFinDate = DateTime.ParseExact(FechaFinal, "yyyy-MM-dd", CultureInfo.InvariantCulture); 

                var Result = objventaBL.GetVentasReporteLista(fechaIniDate, fechaFinDate);
                //response.code = (int)Enums.eCodeError.OK;
                //response.Data = data;
                return Ok(Result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                throw;
            }
        } 

        [HttpGet]
        [Route("ObtenerListadoPollos/{StartDate?}/{EndDate?}")]
        public IActionResult ObtenerListadoPollos(string StartDate, string EndDate)
        {
            try
            {
                //Obtener el Token
                string secretTokenName = "Auth-Token";
                var tokenCode = HttpContext.Request.Headers[secretTokenName].ToString();
                DateTime fechaIniDate = DateTime.ParseExact(StartDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime fechaFinDate = DateTime.ParseExact(EndDate, "yyyy-MM-dd", CultureInfo.InvariantCulture) ;
                var Result = objventaBL.ObtenerListadoPollos(fechaIniDate, fechaFinDate);
                //response.code = (int)Enums.eCodeError.OK;
                //response.Data = data;
                return Ok(Result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                throw;
            }
        }
         
        [HttpPost]
        [Route("GuardarGuiaVenta")]
        public IActionResult GuardarGuiaVenta(GuiaVentaPolloRequest request)
        {
            try
            {
                GenericResponse response = new GenericResponse();
                int resul = objventaBL.GuardarGuiaVenta(request);
                return Ok(resul);       
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                throw ex;
            }
        }




    }
}
