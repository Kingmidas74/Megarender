namespace Megarender.DataStorage
{
    public record RedisSettings
    {
        public bool Enabled { get; init; }
        public string ConnectionString { get; set; }
    }
}