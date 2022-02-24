using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSanFelipe.Utilities.Rest
{
  public   class RestClient
    {
        private static string IPProxy;
        private static int PortProxy;

        public RestClient(string IP = "", int Port = 0)
        {
            IPProxy = IP;
            PortProxy = Port;
        }

        public static string Get(string Url, string Authorization = "")
        {

            var Request = (HttpWebRequest)WebRequest.Create(Url);
            Request.Method = "GET";
            Request.ContentType = "application/json";
            if (!string.IsNullOrEmpty(Authorization))
                Request.Headers["Auth-Token"] = Authorization;
            var Response = (HttpWebResponse)Request.GetResponse();
            var ResponseString = new StreamReader(Response.GetResponseStream()).ReadToEnd();
            return ResponseString;
        }


        public static string Post(string Url, string Json, string Authorization = "")
        {

            var Request = (HttpWebRequest)WebRequest.Create(Url);
            var Data = Encoding.UTF8.GetBytes(Json);
            Request.Method = "POST";
            Request.ContentType = "application/json";
            if (!string.IsNullOrEmpty(Authorization))
                Request.Headers["Auth-Token"] = Authorization;
            Request.ContentLength = Data.Length;
            using (var Stream = Request.GetRequestStream())
            {
                Stream.Write(Data, 0, Data.Length);
            }
            var Response = (HttpWebResponse)Request.GetResponse();
            var ResponseString = new StreamReader(Response.GetResponseStream()).ReadToEnd();
            return ResponseString;
        }


        public static object ProcessPostRequest(string Url, object Request, string Authorization = "")
        {
            var Process = Post(Url, JsonConvert.SerializeObject(Request), Authorization);
            try
            {
                var Response = JsonConvert.DeserializeObject<object>(Process);
                return Response;
            }
            catch (Exception ex)
            {
                var Response = Process;
                return Response;
            }

        }

        public static object ProcessGetRequest(string Url, string Authorization = "")
        {
            var Process = Get(Url, Authorization);
            var Response = JsonConvert.DeserializeObject<object>(Process);
            return Response;
        }

        public string UploadPost(string URLService, string Method, string FullPath, string fileName)
        {
            /*var handler = new HttpClientHandler { UseDefaultCredentials = true };
            WebProxy proxyObject;
            proxyObject = new WebProxy("10.48.253.136", 8080);
            proxyObject.BypassProxyOnLocal = false;
            handler.Proxy = proxyObject;*/

            //using (var client = new HttpClient(handler))
            using (var client = new HttpClient())
            {
                /*
                var fileContent = new ByteArrayContent(bytes);
                
                var content = new MultipartFormDataContent { 
                    { fileContent, "file", fileName }
                };
                fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = fileName };
                content.Add(content);*/

                client.BaseAddress = new Uri(URLService);
                client.Timeout = TimeSpan.FromMinutes(10);
                //client.DefaultRequestHeaders.Accept.Clear();
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add("username", "benemanuel");

                MultipartFormDataContent form = new MultipartFormDataContent();
                HttpContent content = new StringContent("fileToUpload");
                HttpContent DictionaryItems = new FormUrlEncodedContent(parameters);
                form.Add(content, "fileToUpload");
                form.Add(DictionaryItems, "medicineOrder");

                var stream = new FileStream(FullPath, FileMode.Open);
                content = new StreamContent(stream);
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "fileToUpload",
                    FileName = fileName
                };
                form.Add(content);

                var result = client.PostAsync(Method, form).Result;
                //Log.WriteLogInfo(result.ToString());
                if (result.IsSuccessStatusCode)
                {
                    var resultContent = result.Content.ReadAsStringAsync().Result;
                    //Log.WriteLogInfo(resultContent);

                    return resultContent;
                }
            }
            return string.Empty;
        }

    }
}
