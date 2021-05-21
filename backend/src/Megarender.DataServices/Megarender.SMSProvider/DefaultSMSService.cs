using System.Threading.Tasks;

namespace Megarender.SMS
{
    public class DefaultSMSService:ISMSService
    {
        public Task<bool> SendMessageAsync(string number, string text)
        {
            return Task.FromResult<bool>(false);
        }
    }
}