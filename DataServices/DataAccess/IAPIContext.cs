using System.Threading.Tasks;
using Megarender.Domain;
using Microsoft.EntityFrameworkCore;

namespace Megarender.DataAccess
{
    public interface IAPIContext 
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();

    }
}