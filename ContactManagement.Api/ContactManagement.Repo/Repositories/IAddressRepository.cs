using ContactManagement.DAL.Entities;
using ContactManagement.Repo.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactManagement.Repo.Repositories
{
    public interface IAddressRepository : IRepository<Address, long>
    {
        Task<List<Address>> GetAllAsync();
        Task<AddressDTO> GetByIdAsync(long id);
    }
}
