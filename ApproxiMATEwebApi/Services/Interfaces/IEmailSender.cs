using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Services.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
