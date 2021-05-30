using System;
using Microsoft.EntityFrameworkCore;

namespace Megarender.IdentityService {
    public class AppDbContext : DbContext 
    {
        private readonly string SchemaName = "identity";
        public AppDbContext (DbContextOptions<AppDbContext> options) : base (options) { }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.HasDefaultSchema (schema: SchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating (modelBuilder);
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Identity> Identities { get; set; }
    }

    public class User {
        public Guid Id { get; init; }
        
        public CommunicationChannelId PreferredCommunicationChannel { get; init; }
        public CommunicationChannelsData CommunicationChannelsData { get; init; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }

    public class Identity {
        public Guid Id { get; init; }
        public string Phone { get; init; }
        public string Code {get;set;}
    }
    
    public record CommunicationChannelsData
    {
        public string PhoneNumber { get; init; }
        public string Email { get; init; }
        public string TelegramId { get; init; }
    }
}