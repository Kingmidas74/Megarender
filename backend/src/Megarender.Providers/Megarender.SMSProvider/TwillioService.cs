using System.Threading.Tasks;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Megarender.SMS
{
    public class TwillioService:ISMSService
    {
        private readonly ITwilioRestClient _client;
        private readonly SMSSettings _smsSettings;

        public TwillioService(ITwilioRestClient client, SMSSettings smsSettings)
        {
            _client = client;
            _smsSettings = smsSettings;
        }

        public async Task<bool> SendMessageAsync(string phone, string text)
        {
            var message = await MessageResource.CreateAsync(
                new PhoneNumber(phone),
                from: new PhoneNumber(_smsSettings.Sender),
                body: text,
                client: _client);
            if (message.ErrorCode.HasValue)
            {
                //throw new Exception(message.ErrorMessage);
            }
            return message.ErrorCode.HasValue;
        }
    }
}