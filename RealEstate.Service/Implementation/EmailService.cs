using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Service.Interface;

using RealEstate.Domain;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mail;

namespace RealEstate.Service.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;

        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public Boolean SendEmailAsync(EmailMessage emailMessageInput)
        {
            var emailMessage = new MimeMessage
            {
                Subject = emailMessageInput.Subject,
                Sender = new MailboxAddress("Real Estate App", _mailSettings.SmtpUserName)
            };

          
            emailMessage.From.Add(new MailboxAddress("Real Estate App", _mailSettings.SmtpUserName));

            emailMessage.To.Add(new MailboxAddress(emailMessageInput.MailTo, emailMessageInput.MailTo));

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = emailMessageInput.Content
            };

            try
            {
                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    smtp.Connect(_mailSettings.SmtpServer, 587, SecureSocketOptions.Auto);

                    if (!string.IsNullOrEmpty(_mailSettings.SmtpUserName))
                    {
                        smtp.Authenticate(_mailSettings.SmtpUserName, _mailSettings.SmtpPassword);
                    }

                    smtp.Send(emailMessage);
                    smtp.Disconnect(true);

                    return true;
                }
            }
            catch (SmtpException ex)
            {
                throw; 
            }
        }

    }
}
