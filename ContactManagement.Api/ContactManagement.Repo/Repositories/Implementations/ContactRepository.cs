using ContactManagement.DAL;
using ContactManagement.DAL.Entities;
using ContactManagement.Repo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ContactManagement.Repo.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactDBContext _dbContext;
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

        public async Task<Contact> FindByIdAsync(long id)
        {
            return await (from dcContact in _dbContext.Contact
                           .Include(b => b.ContactEnterprise).ThenInclude(b => b.Enterprise)
                          where dcContact.Id == id
                          select dcContact)
                          .SingleOrDefaultAsync();
        }

        public async Task<ContactDTO> GetByIdAsync(long id)
        {
            return await (from dcContact in _dbContext.Contact
                           .Include(b => b.ContactEnterprise).ThenInclude(b => b.Enterprise)
                          where dcContact.Id == id
                          select dcContact)
                          .Select(SelectContact)
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
            var isUpdate = await this._dbContext.Contact.AnyAsync(x => x.Id == contact.Id);
            if (isUpdate)
            {
                await this.ReplaceAsync(contact);
            }
           
            await this.CreateAsync(contact);
        }

        private Expression<Func<Contact, ContactDTO>> SelectContact = (item =>
          new ContactDTO()
          {
              Id = item.Id,
              Adress = new AdressDTO
              {
                  Id = item.Adress.Id,
                  City = item.Adress.City,
                  Country = item.Adress.Country,
                  Name = item.Adress.Name,
                  PostalCode = item.Adress.PostalCode,
                  Street = item.Adress.Street,
                  StreetNumber = item.Adress.StreetNumber
              },
              FirstName = item.FirstName,
              LastName = item.LastName,
              GSMNumber = item.GSMNumber,
              IsFreelance = item.IsFreelance,
              TVANumber = item.TVANumber,
              Enterprises = item.ContactEnterprise.Select(x => new EnterpriseDTOBase()
              { 
                    Id = x.Enterprise.Id,
                    Name = x.Enterprise.Name,
                    TVANumber = x.Enterprise.TVANumber
              }).ToList()
             
          });
    }
}
