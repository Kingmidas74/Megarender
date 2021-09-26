using System;
using AutoFixture;
using Megarender.DataAccess;

namespace Megarender.Features.UnitTests
{
    public abstract class TestBaseFixture : IDisposable
    {
        protected TestBaseFixture()
        {
            Context = DbContextFactory.Create();
            Fixture = new Fixture();
            Fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        protected Fixture Fixture { get; }
        protected APIContext Context { get; }

        public void Dispose()
        {
            DbContextFactory.Destroy(Context);
        }
    }
}