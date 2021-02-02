using System;
using System.Threading.Tasks;

namespace Megarender.MemoryStorage
{
    public interface ICommandStore
    {
        Task<bool> Exists(Guid id);
        Task Save(Guid id);
    }
}
