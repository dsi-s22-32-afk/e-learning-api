using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using UniWall.Utilities;

namespace UniWall.Utilities
{
   

    public class EmailService
    {
 
        public async Task SendAsync(string to, string subject, string html)
        {
            IConfiguration _appSettings = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            string smtpHost = _appSettings.GetValue<string>("NotificationMetadata:Host");
            string smtpPort = _appSettings.GetValue<string>("NotificationMetadata:Port");
            string smtpUser = _appSettings.GetValue<string>("NotificationMetadata:UserName");
            string smtpPass = _appSettings.GetValue<string>("NotificationMetadata:Password");
   
          
            var client = new System.Net.Mail.SmtpClient(smtpHost, int.Parse(smtpPort))
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            try
            {
                await client.SendMailAsync(
                    new MailMessage("filipsz11@gmail.com", to, subject, html) { IsBodyHtml = true }
                );
            }
            catch (Exception ex)
            {

            }
        }
    }
}
