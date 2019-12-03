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
    public class AdressRepository : IAdressRepository
    {
        private readonly ContactDBContext _dbContext;
        public AdressRepository(ContactDBContext dbContext)
        {
            _dbContext = dbContext;
           
        }

        public async Task CreateAsync(Adress adress)
        {
            if (_dbContext.Entry(adress).State == EntityState.Detached)
            {
                await _dbContext.Adress.AddAsync(adress);
            }
            if (_dbContext.Entry(adress).State == EntityState.Added)
            {

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Adress adress)
        {
            if (_dbContext.Entry(adress).State == EntityState.Detached)
            {
                _dbContext.Adress.Attach(adress);
            }

            _dbContext.Adress.Remove(adress);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<AdressDTO> GetByIdAsync(long id)
        {
            return await (from dbAdress in _dbContext.Adress
                           .Include(b => b.EnterpriseAdress).ThenInclude(b => b.Enterprise)
                          where dbAdress.Id == id
                          select dbAdress)
                          .Select(SelectAdress)
                          .SingleOrDefaultAsync();
        }

        public async Task<Adress> FindByIdAsync(long id)
        {
            return await (from dbAdress in _dbContext.Adress
                           .Include(b => b.EnterpriseAdress).ThenInclude(b => b.Enterprise)
                          where dbAdress.Id == id
                          select dbAdress)
                          .SingleOrDefaultAsync();
        }

        public async Task<List<Adress>> GetAllAsync()
        {
            return await (from dbAdress in _dbContext.Adress
                           .Include(b => b.EnterpriseAdress).ThenInclude(b => b.Enterprise)
                          select dbAdress)
                          .ToListAsync();
        }

        public async Task ReplaceAsync(Adress adress)
        {
            if (_dbContext.Entry(adress).State == EntityState.Detached)
            {
                _dbContext.Entry(adress).State = EntityState.Modified;
            }

            await _dbContext.SaveChangesAsync();

        }

        public async Task UpsertAsync(Adress adress)
        {
            var isUpdate = await this._dbContext.Adress.AnyAsync(x => x.Id == adress.Id);
            if (isUpdate)
            {
                await this.ReplaceAsync(adress);
            }
            await this.CreateAsync(adress);
        }

        private Expression<Func<Adress, AdressDTO>> SelectAdress = (item =>
          new AdressDTO()
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
