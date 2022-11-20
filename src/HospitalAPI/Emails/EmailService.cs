using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using HospitalLibrary.Shared.Interfaces;

namespace HospitalAPI.Emails
{
    public class EmailService : IEmailService
    {
        public async Task SendAppointmentEmail(string email)
        {
            MailMessage newMail = new MailMessage();
            newMail.From = new MailAddress("levi.five.hospital@gmail.com", "Levi Five");
            newMail.To.Add(email);
            newMail.Subject = "Appointment assigned!";
            newMail.Body = "<h2>Your appointment has been successfully added!<h2/><br/>" +
                            "<h2><strong>Sent from LEVI-FIVE Hospital Service!</strong></h2>";
            newMail.IsBodyHtml = true;
            
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            NetworkCredential netCre = new NetworkCredential("levi.five.hospital@gmail.com", "fnaafrzlyeaxqupx");
            client.Credentials = netCre;
            client.Send(newMail);
        }

        public async Task SendWelcomeEmail(string email)
        {
            MailMessage newMail = new MailMessage();
            newMail.From = new MailAddress("levi.five.hospital@gmail.com", "Levi Five");
            newMail.To.Add(email);
            newMail.Subject = "Welcome!";
            newMail.Body = "<h2>Welcome to LEVI-FIVE Hospital Service!<h2/><br/>" +
                           "<h2><strong>Sent from LEVI-FIVE Hospital Service!</strong></h2>";
            newMail.IsBodyHtml = true;
            
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            NetworkCredential netCre = new NetworkCredential("levi.five.hospital@gmail.com", "fnaafrzlyeaxqupx");
            client.Credentials = netCre;
            client.Send(newMail);
        }

        public async Task SendWelcomeEmailWithActivationLink(string email)
        {
            MailMessage newMail = new MailMessage();
            newMail.Subject = "Welcome to LeviFive hospital!";
            newMail.From = new MailAddress("levi.five.hospital@gmail.com", "Levi Five");
            newMail.To.Add(email);
            newMail.Body = "<h1>Welcome to LEVI-FIVE Hospital Service!</h1> <br/>" +
                           "<h2><a href=\"https://www.youtube.com/watch?v=dQw4w9WgXcQ\">Here is your Prize!<a/></h2> <br/>" +
                           "<h2><strong>Sent from LEVI-FIVE Hospital Service!</strong></h2>";
            newMail.IsBodyHtml = true;
            
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            NetworkCredential netCre = new NetworkCredential("levi.five.hospital@gmail.com", "fnaafrzlyeaxqupx");
            client.Credentials = netCre;
            client.Send(newMail);
        }

        public async Task SendAppointmentCanceledEmail(string email, DateTime time)
        {
            MailMessage newMail = new MailMessage();
            newMail.Subject = "Canceled appointment!";
            newMail.From = new MailAddress("levi.five.hospital@gmail.com", "Levi Five");
            newMail.To.Add(email);
            newMail.Body = "<h1>Your appointment for " + time +"has been canceled</h1> <br/>" +
                           "<h2><strong>Sent from LEVI-FIVE Hospital Service!</strong></h2>";
            newMail.IsBodyHtml = true;
            
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            NetworkCredential netCre = new NetworkCredential("levi.five.hospital@gmail.com", "fnaafrzlyeaxqupx");
            client.Credentials = netCre;
            client.Send(newMail);
        }

        public async Task SendAppointmentRescheduledEmail(string email, DateTime time, DateTime newTime)
        {
            MailMessage newMail = new MailMessage();
            newMail.Subject = "Rescheduled appointment!";
            newMail.From = new MailAddress("levi.five.hospital@gmail.com", "Levi Five");
            newMail.To.Add(email);
            newMail.Body = "<h1>Your appointment for " + time +"has been moved to"+ newTime +"</h1> <br/>" +
                           "<h2><strong>Sent from LEVI-FIVE Hospital Service!</strong></h2>";
            newMail.IsBodyHtml = true;
            
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            NetworkCredential netCre = new NetworkCredential("levi.five.hospital@gmail.com", "fnaafrzlyeaxqupx");
            client.Credentials = netCre;
            client.Send(newMail);
        }
    }
}