using ContactManagement.DAL.Entities;
using ContactManagement.Repo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.Repo.Services
{
    public interface IEnterpriseService
    {
        Task<IEnumerable<Enterprise>> ListAsync();
        Task<Enterprise> GetByIdAsync(long id);
        Task<Enterprise> SaveAsync(EnterpriseDTOFull enterpriseDTO);
        Task DeleteAsync(long id);
    }
}
