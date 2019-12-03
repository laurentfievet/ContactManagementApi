using ContactManagement.DAL.Entities;
using ContactManagement.Repo.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactManagement.Repo.Services
{
    public interface IEnterpriseService
    {
        Task<IEnumerable<Enterprise>> ListAsync();
        Task<EnterpriseDTOFull> GetByIdAsync(long id);
        Task<Enterprise> SaveAsync(EnterpriseDTOFull enterpriseDTO);
        Task<Enterprise> AddAdressesAsync(long enterpriseId, List<EnterpriseAdressDTO> enterpriseDTOList);
        Task DeleteAsync(long id);
    }
}
