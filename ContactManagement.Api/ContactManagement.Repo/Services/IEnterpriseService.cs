using ContactManagement.DAL.Entities;
using ContactManagement.Repo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactManagement.Repo.Services
{
    public interface IEnterpriseService
    {
        Task<IEnumerable<Enterprise>> ListAsync();
        Task<EnterpriseDTOFull> GetByIdAsync(long id);
        Task<Enterprise> SaveAsync(EnterpriseDTOFull enterpriseDTO);
        Task<Enterprise> AddAddressesAsync(long enterpriseId, List<EnterpriseAddressDTO> enterpriseDTOList);
        Task DeleteAsync(long id);
    }
}
