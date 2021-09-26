using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Megarender.Telegram
{
    public class BotService:IBotService
    {
        public BotService(BotSettings config)
        {
            Client = new TelegramBotClient(config.BotToken);
            Client.SetWebhookAsync(config.WebHookURI).GetAwaiter().GetResult();
        }
        private TelegramBotClient Client { get; }
        
        public async Task<bool> SendTextMessageAsync(int id, string message)
        {
            return (await Client.SendTextMessageAsync(new ChatId(id), message)).MessageId>0;
        }
    }
}