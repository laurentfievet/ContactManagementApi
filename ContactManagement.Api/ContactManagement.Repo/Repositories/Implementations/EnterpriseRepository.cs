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
        private ContactDBContext _dbContext;
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
                           .Include(b => b.EnterpriseAdress).ThenInclude(b => b.Adress)
                          where dbEnterprise.Id == id
                          select dbEnterprise)
                          .SingleOrDefaultAsync();
        }

        public async Task<EnterpriseDTOFull> GetByIdAsync(long id)
        {
            return await (from dbEnterprise in _dbContext.Enterprise
                          .Include(b => b.ContactEnterprise).ThenInclude(b => b.Contact)
                           .Include(b => b.EnterpriseAdress).ThenInclude(b => b.Adress)
                          where dbEnterprise.Id == id
                          select dbEnterprise)
                          .Select(SelectEnterprise)
                          .SingleOrDefaultAsync();
        }

        public async Task<List<Enterprise>> GetAllAsync()
        {
            return await (from dbEnterprise in _dbContext.Enterprise
                           .Include(b => b.ContactEnterprise).ThenInclude(b => b.Contact)
                           .Include(b => b.EnterpriseAdress).ThenInclude(b => b.Adress)
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
            if (enterprise.Id == 0)
            {
                await CreateAsync(enterprise);
            }
            else
            {
                await ReplaceAsync(enterprise);
            }
        }

        private Expression<Func<Enterprise, EnterpriseDTOFull>> SelectEnterprise = (item =>
         new EnterpriseDTOFull()
         {
             Id = item.Id,
             TVANumber = item.TVANumber,
             Name = item.Name,
             Adresses = item.EnterpriseAdress.Select(x => new EnterpriseAdressDTO()
             {
                 Id = x.Adress.Id,
                 City = x.Adress.City,
                 Country = x.Adress.Country,
                 Name = x.Adress.Name,
                 PostalCode = x.Adress.PostalCode,
                 Street = x.Adress.Street,
                 StreetNumber = x.Adress.StreetNumber,
                 HeadOffice = x.HeadOffice
             }).ToList()




             //Adress = new AdressDTO
             //{
             //    Id = item.Adress.Id,
             //    City = item.Adress.City,
             //    Country = item.Adress.Country,
             //    Name = item.Adress.Name,
             //    PostalCode = item.Adress.PostalCode,
             //    Street = item.Adress.Street,
             //    StreetNumber = item.Adress.StreetNumber
             //},
             //FirstName = item.FirstName,
             //LastName = item.LastName,
             //GSMNumber = item.GSMNumber,
             //IsFreelance = item.IsFreelance,

             //Enterprises = item.ContactEnterprise.Select(x => new EnterpriseDTOBase()
             //{
             //    Id = x.Enterprise.Id,
             //    Name = x.Enterprise.Name,
             //    TVANumber = x.Enterprise.TVANumber
             //}).ToList()

         });
    }
}
