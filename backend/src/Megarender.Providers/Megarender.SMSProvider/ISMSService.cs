using System.Threading.Tasks;

namespace Megarender.SMS
{
    public interface ISMSService
    {
        Task<bool> SendMessageAsync(string number, string text); 
    }
}