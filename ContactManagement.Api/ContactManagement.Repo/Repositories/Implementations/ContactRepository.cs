using ContactManagement.DAL;
using ContactManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagement.Repo.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private ContactDBContext _dbContext;
        public ContactRepository(ContactDBContext dbContext)
        {
            _dbContext = dbContext;
           
        }

        public async Task CreateAsync(Contact contact)
        {
            if (_dbContext.Entry(contact).State == EntityState.Detached)
            {
                await _dbContext.Contact.AddAsync(contact);
            }
            if (_dbContext.Entry(contact).State == EntityState.Added)
            {

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Contact contact)
        {
            if (_dbContext.Entry(contact).State == EntityState.Detached)
            {
                _dbContext.Contact.Attach(contact);
            }

            _dbContext.Contact.Remove(contact);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Contact> GetByIdAsync(long id)
        {
            return await (from dcContact in _dbContext.Contact
                           .Include(b => b.ContactEnterprise).ThenInclude(b => b.Enterprise)
                          where dcContact.Id == id
                          select dcContact)
                          .SingleOrDefaultAsync();
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            return await (from dbContact in _dbContext.Contact
                           .Include(b => b.ContactEnterprise).ThenInclude(b => b.Enterprise)
                          select dbContact)
                          .ToListAsync();
        }


        public async Task ReplaceAsync(Contact contact)
        {
            if (_dbContext.Entry(contact).State == EntityState.Detached)
            {
                _dbContext.Entry(contact).State = EntityState.Modified;
            }

            await _dbContext.SaveChangesAsync();

        }

        public async Task UpsertAsync(Contact contact)
        {
            if (contact.Id == 0)
            {
                await CreateAsync(contact);
            }
            else
            {
                await ReplaceAsync(contact);
            }
        }
    }
}
