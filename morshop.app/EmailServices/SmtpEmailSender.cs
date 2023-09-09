using System.Net;
using System.Net.Mail;

namespace morshop.app.EmailServices
{
    public class SmtpEmailSender : IEmailSender
    {
        private string _host;
        private int _port;
        private bool _enableSsl;
        private string _userName;
        private string _password;
        public SmtpEmailSender(string host="smtp.office365.com",int port=587,bool enableSsl=true,string userName="abc@example.com",string password="12345")
        {
            this._host=host;
            this._port=port;
            this._enableSsl=enableSsl;
            this._userName=userName;
            this._password=password;
        }

        public Task SenderEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(this._host,this._port)
            {
                Credentials=new NetworkCredential(this._userName,this._password),
                EnableSsl=this._enableSsl
            };
            var mailMessage= new MailMessage(this._userName,email)
            {
                Subject=subject,
                Body=htmlMessage,
                IsBodyHtml=true
            };

            return client.SendMailAsync(mailMessage);
        }
    }
}