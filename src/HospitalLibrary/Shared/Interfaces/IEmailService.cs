using System.Threading.Tasks;

namespace HospitalLibrary.Shared.Interfaces
{
    public interface IEmailService
    {
        Task SendAppointmentEmail(string email);
        Task SendWelcomeEmail(string email);
        Task SendCancelEmail(string email, string time);
    }
}