using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaSanFelipe.Utilities.Request
{
   public  class MailRequest
    {
        public List<string> To { get; set; }
        public List<string> CC { get; set; }
        public List<string> BCC { get; set; }
        public string HtmlMessage { get; set; }
        public string TextMessage { get; set; }
        public string Subject { get; set; }
        public List<string> Attachments { get; set; }
        public MailSettings Settings { get; set; }

        public MailRequest()
        {
            To = new List<string>();
            CC = new List<string>();
            BCC = new List<string>();
            Attachments = new List<string>();
            Settings = new MailSettings();
        }
    }
}
