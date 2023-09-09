namespace morshop.app.EmailServices
{
    public interface IEmailSender
    {
        //smtp = gmail,hotmail
        //api = sendgrid
        Task SenderEmailAsync(string email, string subject, string htmlMessage);
    }
}