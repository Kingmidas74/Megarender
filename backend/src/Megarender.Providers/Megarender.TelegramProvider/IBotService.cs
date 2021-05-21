using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Megarender.Telegram
{
    public interface IBotService
    {
        Task<bool> SendTextMessageAsync(int id, string message);
    }
}