using Microsoft.AspNetCore.Identity.UI.Services;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace BookStoreMVCWeb.Services
{
    public class EmailSender 
    {
		//public bool SendEmail(string userEmail, string confirmationLink)
		//{
		//	MailMessage mailMessage = new MailMessage();
		//	mailMessage.From = new MailAddress("bakhaipth@gmail.com");
		//	mailMessage.To.Add(new MailAddress(userEmail));

		//	mailMessage.Subject = "Confirm your email";
		//	mailMessage.IsBodyHtml = true;
		//	mailMessage.Body = confirmationLink;

		//	SmtpClient client = new SmtpClient();
		//	client.Credentials = new System.Net.NetworkCredential("bakhaipth@gmail.com", "Bakhai240202");
		//	client.Host = "smtp.gmail.com";
		//	client.Port = 7246;

		//	try
		//	{
		//		client.Send(mailMessage);
		//		return true;
		//	}
		//	catch (Exception ex)
		//	{
		//		// log exception
		//	}
		//	return false;
		//}
		public IConfiguration _configuration { get; }
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendEmail(string email, string subject, string HtmlMessage)
        {
            using (MailMessage mm = new MailMessage(_configuration["NetMail:sender"], email))
            {
                mm.Subject = subject;
                string body = HtmlMessage;
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = _configuration["NetMail:smtpHost"];
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(_configuration["NetMail:sender"], _configuration["NetMail:senderpassword"]);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                
            }
        }
    }
}
