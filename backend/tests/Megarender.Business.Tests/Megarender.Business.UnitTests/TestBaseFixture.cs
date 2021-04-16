using System;
using Megarender.DataAccess;

namespace Megarender.Business.UnitTests
{
    public abstract class TestBaseFixture : IDisposable
    {
        protected TestBaseFixture()
        {
            Context = DbContextFactory.Create();
        }

        protected APIContext Context { get; }

        public void Dispose()
        {
            DbContextFactory.Destroy(Context);
        }
    }
}