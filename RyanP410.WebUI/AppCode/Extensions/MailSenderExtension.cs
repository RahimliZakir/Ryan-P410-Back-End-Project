﻿using System.Net;
using System.Net.Mail;

namespace RyanP410.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static bool SendMail(this IConfiguration configuration, string fromMail, string password, string toMail, string subject, string body, string? cc = null, int port = 25, string host = "smtp.mail.ru")
        {
            try
            {
                SmtpClient client = new()
                {
                    Host = host,
                    EnableSsl = true,
                    Port = port
                };

                client.Credentials = new NetworkCredential(fromMail, password);

                MailMessage message = new(fromMail, toMail);

                message.Subject = subject;
                message.Body = body;

                if (!string.IsNullOrWhiteSpace(cc))
                {
                    message.CC.Add(cc);
                }

                message.IsBodyHtml = true;

                client.Send(message);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}