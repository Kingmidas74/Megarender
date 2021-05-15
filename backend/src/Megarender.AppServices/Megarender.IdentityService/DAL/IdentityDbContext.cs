using System;
using Microsoft.EntityFrameworkCore;

namespace Megarender.IdentityService {
    public class AppDbContext : DbContext {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base (options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Identity> Identities { get; set; }
    }

    public class User {
        public Guid Id { get; init; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Phone { get; init; }
    }

    public class Identity {
        public Guid Id { get; init; }
        public string Phone { get; init; }
        public string Code {get;set;}
    }
}