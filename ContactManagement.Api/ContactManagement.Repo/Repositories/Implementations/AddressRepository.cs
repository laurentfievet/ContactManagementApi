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
    public class AddressRepository : IAddressRepository
    {
        private readonly ContactDBContext _dbContext;
        public AddressRepository(ContactDBContext dbContext)
        {
            _dbContext = dbContext;
           
        }

        public async Task CreateAsync(Address adress)
        {
            if (_dbContext.Entry(adress).State == EntityState.Detached)
            {
                await _dbContext.Address.AddAsync(adress);
            }
            if (_dbContext.Entry(adress).State == EntityState.Added)
            {

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Address adress)
        {
            if (_dbContext.Entry(adress).State == EntityState.Detached)
            {
                _dbContext.Address.Attach(adress);
            }

            _dbContext.Address.Remove(adress);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<AddressDTO> GetByIdAsync(long id)
        {
            return await (from dbAddress in _dbContext.Address
                           .Include(b => b.EnterpriseAddress).ThenInclude(b => b.Enterprise)
                          where dbAddress.Id == id
                          select dbAddress)
                          .Select(SelectAddress)
                          .SingleOrDefaultAsync();
        }

        public async Task<Address> FindByIdAsync(long id)
        {
            return await (from dbAddress in _dbContext.Address
                           .Include(b => b.EnterpriseAddress).ThenInclude(b => b.Enterprise)
                          where dbAddress.Id == id
                          select dbAddress)
                          .SingleOrDefaultAsync();
        }

        public async Task<List<Address>> GetAllAsync()
        {
            return await (from dbAddress in _dbContext.Address
                           .Include(b => b.EnterpriseAddress).ThenInclude(b => b.Enterprise)
                          select dbAddress)
                          .ToListAsync();
        }

        public async Task ReplaceAsync(Address address)
        {
            if (_dbContext.Entry(address).State == EntityState.Detached)
            {
                _dbContext.Entry(address).State = EntityState.Modified;
            }

            await _dbContext.SaveChangesAsync();

        }

        public async Task UpsertAsync(Address adress)
        {
            var isUpdate = await this._dbContext.Address.AnyAsync(x => x.Id == adress.Id);
            if (isUpdate)
            {
                await this.ReplaceAsync(adress);
            }
            await this.CreateAsync(adress);
        }

        private Expression<Func<Address, AddressDTO>> SelectAddress = (item =>
          new AddressDTO()
          {
                Id = item.Id,
                 City = item.City,
                 Country = item.Country,
                 Name = item.Name,
                 PostalCode = item.PostalCode,
                 Street = item.Street,
                 StreetNumber = item.StreetNumber
          });
    }
}
