using System;
using System.Threading.Tasks;

namespace ContactManagement.Repo.Repositories
{
    public interface IRepository<T, TId> where TId : IEquatable<TId> where T : class
    {
        Task<T> FindByIdAsync(TId id);
        Task CreateAsync(T obj);
        Task ReplaceAsync(T obj);
        Task UpsertAsync(T obj);
        Task DeleteAsync(T obj);
    }

}
