﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    public class MainController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger Logger;
        private readonly IHostingEnvironment HostingEnvironment;
        public MainController(IConfiguration IConfiguration, ILoggerFactory LoggerFactory, IHostingEnvironment IHostingEnvironment)
        {
            HostingEnvironment = IHostingEnvironment;
            Configuration = IConfiguration;
            Logger = LoggerFactory.CreateLogger<MainController>();
            GeneralModel.UrlWebApi = Configuration["Urls:UrlWebApi"];
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BandejaComercial()
        {
            return View();
        }
        public IActionResult BandejaVentas()
        {
            return View();
        }

        public IActionResult FichaVenta()
        {
            return View();
        }
         
        public IActionResult GetUserData()
        {
            var Response = new GenericObjectResponse();
            try
            {
                var token = HttpContext.Session.GetString("TOKEN");
                var UrlUser = GeneralModel.UrlWebApi + "Usuario/GetUserDataByToken";
                var ResultUser = SistemaSanFelipe.Utilities.Rest.RestClient.ProcessGetRequest(UrlUser, token);
                var ResultUserJson = JsonConvert.SerializeObject(ResultUser);
                var UserData = JsonConvert.DeserializeObject<UserResponse>(ResultUserJson);
                Response.data = UserData;
            }
            catch (Exception ex)
            {
                Logger.LogWarning(ex, ex.Message);
                Response.message = ex.Message;
            }
            return Ok(Response);
        }

    }
}
