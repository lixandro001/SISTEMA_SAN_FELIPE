using SistemaSanFelipe.Utilities.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SistemaSanFelipe.Utilities.Mail
{
    class MailUtility
    {
        public static string ReadTemplate(string template)
        {
            StreamReader sr = new StreamReader(template);
            string Result = sr.ReadToEnd();
            sr.Close();
            return Result;
        }

        public static string SetParameters(string message, Dictionary<string, string> parameters)
        {
            string result = message;
            foreach (var par in parameters)
            {
                result = result.Replace(par.Key, par.Value);
            }
            return result;
        }

        public static bool SendMail(MailRequest request)
        {
            try
            {
                //Log.WriteLogInfo("Inicio Enviar correo");

                //tomar los datos de conexion del webconfig
                bool flgenviarcorreo = request.Settings.FlgSendMail;

                if (!flgenviarcorreo)
                {
                    //Escribir en log
                    if (request.To.Count > 0)
                    {
                        string paranoenviar = request.To[0];
                    }
                    return false;
                }

                string mailuser = request.Settings.MailUser;
                string mailuserpass = request.Settings.MailUserPass;
                string mailserver = request.Settings.MailServer;
                string mailserverport = request.Settings.MailServerPort;
                bool mailserverssl = request.Settings.MailServerSSL;

                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.From = new MailAddress(mailuser);

                foreach (string para in request.To)
                {
                    mail.To.Add(new MailAddress(para));
                }

                foreach (string copia in request.CC)
                {
                    if (!string.IsNullOrEmpty(copia))
                    {
                        mail.CC.Add(new MailAddress(copia));
                    }
                }

                foreach (string bulkcopia in request.BCC)
                {
                    if (!string.IsNullOrEmpty(bulkcopia))
                    {
                        mail.Bcc.Add(new MailAddress(bulkcopia));
                    }
                }

                foreach (string adjunto in request.Attachments)
                {
                    if (!string.IsNullOrEmpty(adjunto))
                    {
                        if (File.Exists(adjunto))
                        {
                            mail.Attachments.Add(new Attachment(adjunto));
                        }
                    }
                }

                mail.Subject = request.Subject;
                mail.Body = request.HtmlMessage;
                mail.Priority = MailPriority.High;

                string palinBody = request.TextMessage;
                AlternateView plainView = AlternateView.CreateAlternateViewFromString(palinBody, null, "text/plain");

                // then we create the Html part to embed images,
                // we need to use the prefix 'cid' in the img src value
                string htmlBody = request.HtmlMessage;
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null, "text/html");

                // add the views
                mail.AlternateViews.Add(plainView);
                mail.AlternateViews.Add(htmlView);

                SmtpClient clienteenvio = new SmtpClient();
                clienteenvio.UseDefaultCredentials = false;
                clienteenvio.DeliveryMethod = SmtpDeliveryMethod.Network;

                clienteenvio.Credentials = new NetworkCredential(mailuser, mailuserpass);
                clienteenvio.Host = mailserver;
                clienteenvio.Port = Convert.ToInt32(mailserverport);
                clienteenvio.EnableSsl = false;
                if (mailserverssl)
                {
                    clienteenvio.EnableSsl = true;
                }

                try
                {
                    clienteenvio.Send(mail);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
