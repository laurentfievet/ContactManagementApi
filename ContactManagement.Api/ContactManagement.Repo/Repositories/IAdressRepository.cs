using ContactManagement.DAL.Entities;
using ContactManagement.Repo.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactManagement.Repo.Repositories
{
    public interface IAdressRepository : IRepository<Adress, long>
    {
        Task<List<Adress>> GetAllAsync();
        Task<AdressDTO> GetByIdAsync(long id);
    }
}
