using System;
using Megarender.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Megarender.Business.UnitTests
{
    public static class DbContextFactory
    {
        public static APIContext Create() => new(new DbContextOptionsBuilder<APIContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options);

        public static void Destroy(APIContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}