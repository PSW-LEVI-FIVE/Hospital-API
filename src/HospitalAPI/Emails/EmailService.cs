using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalAPI.Emails
{
    public class EmailService : IEmailService
    {
        private static MailMessage GenerateMailMessage(string email)
        {
            MailMessage newMail = new MailMessage();
            newMail.From = new MailAddress("levi.five.hospital@gmail.com", "Levi Five");
            newMail.To.Add(email);
            return newMail;
        }
        private static void SendMail(MailMessage newMail)
        {
            newMail.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            NetworkCredential netCre = new NetworkCredential("levi.five.hospital@gmail.com", "fnaafrzlyeaxqupx");
            client.Credentials = netCre;
            client.Send(newMail);
        }
        public async Task SendAppointmentEmail(string email)
        {
            MailMessage newMail = GenerateMailMessage(email);
            newMail.Subject = "Appointment assigned!";
            newMail.Body = "<h2>Your appointment has been successfully added!<h2/><br/>" +
                            "<h2><strong>Sent from LEVI-FIVE Hospital Service!</strong></h2>";
            SendMail(newMail);
        }

        public async Task SendWelcomeEmail(string email)
        {
            MailMessage newMail = GenerateMailMessage(email);
            newMail.Subject = "Welcome!";
            newMail.Body = "<h2>Welcome to LEVI-FIVE Hospital Service!<h2/><br/>" +
                           "<h2><strong>Sent from LEVI-FIVE Hospital Service!</strong></h2>";
            SendMail(newMail);
        }

        public async Task SendWelcomeEmailWithActivationLink(string email,string code)
        {
            MailMessage newMail = GenerateMailMessage(email);
            newMail.Subject = "Welcome to LeviFive hospital!";
            newMail.Body = "<h1>Welcome to LEVI-FIVE Hospital Service!</h1> <br/>" +
                           "<h2><a href=\"http://localhost:4200/user/activation/"+ code +"\">Activate your account here!<a/></h2> <br/>" +
                           "<h2><strong>Sent from LEVI-FIVE Hospital Service!</strong></h2>";
            SendMail(newMail);
        }

        public async Task SendAppointmentCanceledEmail(string email, DateTime time)
        {
            MailMessage newMail = GenerateMailMessage(email);
            newMail.Subject = "Canceled appointment!";
            newMail.Body = "<h1>Your appointment for " + time + "has been canceled</h1> <br/>" +
                           "<h2><strong>Sent from LEVI-FIVE Hospital Service!</strong></h2>";
            SendMail(newMail);
        }

        public async Task SendAppointmentRescheduledEmail(string email, DateTime time, DateTime newTime)
        {
            MailMessage newMail = GenerateMailMessage(email);
            newMail.Subject = "Rescheduled appointment!";
            newMail.Body = "<h1>Your appointment for " + time + "has been moved to" + newTime + "</h1> <br/>" +
                           "<h2><strong>Sent from LEVI-FIVE Hospital Service!</strong></h2>";
            SendMail(newMail);
        }
    }
}