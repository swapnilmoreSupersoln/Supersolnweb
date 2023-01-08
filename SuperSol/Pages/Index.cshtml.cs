using EASendMail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
 

namespace SuperSol.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public bool hasData;
        public string name = "";
        public string subject = "";
        public string email = "";
        public string message = "";

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public PageResult OnGet(bool hasData1)
        {
            hasData = hasData1;
            return Page();
        }

        public IActionResult OnPost()
        {
            hasData = true;
            name = Request.Form["name"];
            subject = Request.Form["subject"];
            email = Request.Form["email"];
            message = Request.Form["message"];
            SendEmail(message, email, subject);
            return RedirectToPage("/Index", new
            {
                hasData = hasData
            });
        }


        public void SendEmail(string message, string email, string subject)
        {
            try
            {
                SmtpMail oMail = new SmtpMail("TryIt");

                // Your email address
                oMail.From = "swapnil.more@supersoln.com";

                // Set recipient email address
                oMail.To = "apurv.lungade@supersoln.com,pandurang.dhage@supersoln.com";

                // Set email subject
                oMail.Subject = subject;

                // Set email body
                oMail.TextBody = message + " mail from " + "/n" + email;

                // Hotmail/Outlook SMTP server address
                SmtpServer oServer = new SmtpServer("smtp.office365.com");

                // If your account is office 365, please change to Office 365 SMTP server
                // SmtpServer oServer = new SmtpServer("smtp.office365.com");

                // User authentication should use your
                // email address as the user name.
                oServer.User = "swapnil.more@supersoln.com";

                // If you got authentication error, try to create an app password instead of your user password.
                // https://support.microsoft.com/en-us/account-billing/using-app-passwords-with-apps-that-don-t-support-two-step-verification-5896ed9b-4263-e681-128a-a6f2979a7944
                oServer.Password = "Logix@123";

                // use 587 TLS port
                oServer.Port = 587;

                // detect SSL/TLS connection automatically
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                Console.WriteLine("start to send email over TLS...");

                SmtpClient oSmtp = new SmtpClient();
                oSmtp.SendMail(oServer, oMail);

                Console.WriteLine("email was sent successfully!");
            }
            catch (Exception ep)
            {
                Console.WriteLine("failed to send email with the following error:");
                Console.WriteLine(ep.Message);
            }
        }

    }
}
