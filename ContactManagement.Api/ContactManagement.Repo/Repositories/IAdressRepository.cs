using ContactManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.Repo.Repositories
{
    public interface IAdressRepository : IRepository<Adress, long>
    {
        Task<List<Adress>> GetAllAsync();
    }
}
