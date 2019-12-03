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
    public class EnterpriseRepository : IEnterpriseRepository
    {
        private readonly ContactDBContext _dbContext;
        public EnterpriseRepository(ContactDBContext dbContext)
        {
            _dbContext = dbContext;
           
        }

        public async Task CreateAsync(Enterprise enterprise)
        {
            if (_dbContext.Entry(enterprise).State == EntityState.Detached)
            {
                await _dbContext.Enterprise.AddAsync(enterprise);
            }
            if (_dbContext.Entry(enterprise).State == EntityState.Added)
            {

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Enterprise enterprise)
        {
            if (_dbContext.Entry(enterprise).State == EntityState.Detached)
            {
                _dbContext.Enterprise.Attach(enterprise);
            }

            _dbContext.Enterprise.Remove(enterprise);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Enterprise> FindByIdAsync(long id)
        {
            return await (from dbEnterprise in _dbContext.Enterprise
                          .Include(b => b.ContactEnterprise).ThenInclude(b => b.Contact)
                           .Include(b => b.EnterpriseAddress).ThenInclude(b => b.Address)
                          where dbEnterprise.Id == id
                          select dbEnterprise)
                          .SingleOrDefaultAsync();
        }

        public async Task<EnterpriseDTOFull> GetByIdAsync(long id)
        {
            return await (from dbEnterprise in _dbContext.Enterprise
                          .Include(b => b.ContactEnterprise).ThenInclude(b => b.Contact)
                           .Include(b => b.EnterpriseAddress).ThenInclude(b => b.Address)
                          where dbEnterprise.Id == id
                          select dbEnterprise)
                          .Select(SelectEnterprise)
                          .SingleOrDefaultAsync();
        }

        public async Task<List<Enterprise>> GetAllAsync()
        {
            return await (from dbEnterprise in _dbContext.Enterprise
                           .Include(b => b.ContactEnterprise).ThenInclude(b => b.Contact)
                           .Include(b => b.EnterpriseAddress).ThenInclude(b => b.Address)
                          select dbEnterprise)
                          .ToListAsync();
        }


        public async Task ReplaceAsync(Enterprise enterprise)
        {
            if (_dbContext.Entry(enterprise).State == EntityState.Detached)
            {
                _dbContext.Entry(enterprise).State = EntityState.Modified;
            }

            await _dbContext.SaveChangesAsync();

        }

        public async Task UpsertAsync(Enterprise enterprise)
        {
            var isUpdate = await this._dbContext.Enterprise.AnyAsync(x => x.Id == enterprise.Id);
            if (isUpdate)
            {
                await this.ReplaceAsync(enterprise);
            }
            await this.CreateAsync(enterprise);
        }

        private Expression<Func<Enterprise, EnterpriseDTOFull>> SelectEnterprise = (item =>
         new EnterpriseDTOFull()
         {
             Id = item.Id,
             VATNumber = item.VATNumber,
             Name = item.Name,
             Addresses = item.EnterpriseAddress.Select(x => new EnterpriseAddressDTO()
             {
                 Id = x.Address.Id,
                 City = x.Address.City,
                 Country = x.Address.Country,
                 Name = x.Address.Name,
                 PostalCode = x.Address.PostalCode,
                 Street = x.Address.Street,
                 StreetNumber = x.Address.StreetNumber,
                 HeadOffice = x.HeadOffice
             }).ToList()
         });
    }
}
