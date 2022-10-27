using System;
using System.Threading.Tasks;
using HospitalLibrary.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HospitalAPI.Emails
{
    public class SendgridProvider: IEmailService
    {

        private SendGridClient _sendgrid;

        public SendgridProvider()
        {
            Console.WriteLine(Environment.GetEnvironmentVariable("SENDGRID_API_KEY"));
            _sendgrid = new SendGridClient(Environment.GetEnvironmentVariable("SENDGRID_API_KEY"));
        }
        
        public async Task SendAppointmentEmail(string email)
        {
            EmailAddress from = new EmailAddress("levifiveorg@gmail.com");
            EmailAddress to = new EmailAddress(email);
            const string subject = "Appointment assigned!";
            const string plainTextContent = "Your appointment has been successfully added!";
            string htmlContent = $"<strong>Sent from LEVI-FIVE Hospital Service!</strong>";
            
            SendGridMessage message = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await _sendgrid.SendEmailAsync(message);
        }

        public async Task SendWelcomeEmail(string email)
        {
            EmailAddress from = new EmailAddress("levifiveorg@gmail.com");
            EmailAddress to = new EmailAddress(email);
            const string subject = "Welcome!";
            const string plainTextContent = "Welcome to LEVI-FIVE Hospital Service!";
            string htmlContent = $"<strong>Sent from LEVI-FIVE Hospital Service!</strong>";
            
            SendGridMessage message = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await _sendgrid.SendEmailAsync(message);
        }
        
        public async Task SendCancelEmail(string email,string time)
        {
            EmailAddress from = new EmailAddress("levifiveorg@gmail.com");
            EmailAddress to = new EmailAddress(email);
            const string subject = "Canceled appointment!";
            string plainTextContent = "Your appointment for" + time + " has been canceled";
            string htmlContent = $"<strong>Sent from LEVI-FIVE Hospital Service!</strong>";
            
            SendGridMessage message = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await _sendgrid.SendEmailAsync(message);
        }
    }
}