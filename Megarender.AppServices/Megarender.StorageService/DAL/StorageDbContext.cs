using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Megarender.StorageService.DAL {

    public class StorageDbContext : DbContext {
        
        public StorageDbContext (DbContextOptions<StorageDbContext> options) : base (options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StorageSystem>()
                .HasData (
                    Enum.GetValues (typeof (StorageSystemId))
                    .Cast<StorageSystemId>()
                    .Select (e => new StorageSystem () {
                            StorageSystemId = e,
                            Value = e.ToString ()
                    })
                );  

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);                
            modelBuilder.Entity<User>()
                .Property(e=>e.StorageSystem)
                .HasConversion<int> ();
        }
    }

    public class User {
        public Guid Id { get; set; }
        public StorageSystemId StorageSystem { get; set; }
    }

    public enum StorageSystemId : int {
        FTP = 1,
        AZURE = 2
    }

    public class StorageSystem {
        public StorageSystemId StorageSystemId { get; set; }
        public string Value { get; set; }
    }
    
}