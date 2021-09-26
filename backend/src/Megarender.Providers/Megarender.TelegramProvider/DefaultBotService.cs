using System.Threading.Tasks;

namespace Megarender.Telegram
{
    public class DefaultBotService:IBotService
    {
        public Task<bool> SendTextMessageAsync(int id, string message)
        {
            return Task.FromResult(true);
        }
    }
}