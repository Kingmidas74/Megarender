using System.Threading.Tasks;

namespace Megarender.Telegram
{
    public interface IBotService
    {
        Task<bool> SendTextMessageAsync(int id, string message);
    }
}