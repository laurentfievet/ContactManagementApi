using ContactManagement.DAL.Entities;
using ContactManagement.Repo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.Repo.Repositories
{
    public interface IEnterpriseRepository : IRepository<Enterprise, long>
    {
        Task<List<Enterprise>> GetAllAsync();
        Task<EnterpriseDTOFull> GetByIdAsync(long id);
    }
}
