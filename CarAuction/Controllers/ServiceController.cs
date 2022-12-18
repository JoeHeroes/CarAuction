using CarAuction.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace CarAuction.Controllers
{
    public class ServiceController : Controller
    {
        [Route("ContactUs")]
        public IActionResult ContactUs()
        {
            return View();
        }

        [Route("HowToBuy")]
        public IActionResult HowToBuy()
        {
            return View();
        }

        [HttpPost]
        [Route("SendEmail")]
        public IActionResult SendEmail(EmialDto emailDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress(emailDto.Email, "Sender");
                    var receiverEmail = new MailAddress("Copart@wp.pl", "Receiver");
                    var password = "Your Email Password here";
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = emailDto.Subject,
                        Body = emailDto.Body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return View();
        }


        [Route("Services")]
        public IActionResult Services()
        {
            return View();
        }
    }
}
