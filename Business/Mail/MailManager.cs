using Core.Utilities.Results;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Entities.Concrete.Email;

namespace Business.Mail
{
    public class MailManager
    {
        public IResult Send(EmailMessage emailMessage)
        {
            try
            {
                var message = new MimeMessage();
                message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
                message.From.Add(new MailboxAddress("Finder Mobile", "finder.mobile@yandex.com"));
                message.Subject = emailMessage.Subject;
                var messageBody = emailMessage.Content;
                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = messageBody,
                };
                using (var emailClient = new SmtpClient())
                {
                    emailClient.Connect(
                        "smtp.yandex.com",
                        465,
                        true);
                    emailClient.Authenticate("finder.mobile@yandex.com", "finderMobile");
                    emailClient.Send(message);
                    emailClient.Disconnect(true);
                }
                return new SuccessResult();
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }

        }
    }
}
