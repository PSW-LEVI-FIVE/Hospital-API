using System;
using System.Threading.Tasks;

namespace HospitalLibrary.Shared.Interfaces
{
    public interface IEmailService
    {
        Task SendAppointmentEmail(string email);
        Task SendWelcomeEmail(string email);
        Task SendWelcomeEmailWithActivationLink(string email);

        Task SendAppointmentCanceledEmail(string email, DateTime time);

        Task SendAppointmentRescheduledEmail(string email, DateTime time, DateTime newTime);
    }
}