using ContactManagement.DAL;
using ContactManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

        public async Task<Enterprise> GetByIdAsync(long id)
        {
            return await (from dbEnterprise in _dbContext.Enterprise
                          .Include(b => b.ContactEnterprise).ThenInclude(b => b.Contact)
                           .Include(b => b.EnterpriseAdress).ThenInclude(b => b.Adress)
                          where dbEnterprise.Id == id
                          select dbEnterprise)
                          .SingleOrDefaultAsync();
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
    }
}
