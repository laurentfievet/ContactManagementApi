using ContactManagement.DAL.Entities;
using ContactManagement.Repo.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactManagement.Repo.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> ListAsync();
        Task<ContactDTO> GetByIdAsync(long id);
        Task<Contact> SaveAsync(ContactDTO contactDTO);
        Task DeleteAsync(long id);
    }
}
