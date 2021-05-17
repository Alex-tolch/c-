using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;

namespace lab4
{
    class Email
    {
        public static void sendingMessage(string recipient, string smtpServer, string from, string password, string caption, string message, List<string> files)
        {
            try
            {
                var mail = new MailMessage(from, recipient, caption, message);

                if (files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        mail.Attachments.Add(new Attachment(files[i]));
                    }
                }
                SmtpClient client = new SmtpClient(smtpServer, 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(from.Split('@')[0], password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                

                MessageBox.Show("Отправлено:\n" + recipient);
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);            
            }
        }
    }
}
