namespace Megarender.IdentityService
{
    public enum CommunicationChannelId
    {
        Phone = 1,
        Email = 2,
        Telegram = 3
    }
    public class CommunicationChannel {
        public CommunicationChannelId CommunicationChannelId { get; set; }
        public string Value { get; set; }
    }
}