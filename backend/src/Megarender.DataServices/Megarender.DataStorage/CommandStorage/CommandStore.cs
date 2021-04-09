using System;
using System.Threading.Tasks;

namespace Megarender.DataStorage
{
    public class CommandStore : ICommandStore
    {
        public Task<bool> Exists(Guid id)
        {
            return Task.FromResult(false);
        }

        public Task Save(Guid id)
        {
            return Task.CompletedTask;
        }
    }
}
