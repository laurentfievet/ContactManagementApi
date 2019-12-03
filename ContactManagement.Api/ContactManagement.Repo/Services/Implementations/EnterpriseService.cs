using AutoMapper;
using ContactManagement.DAL;
using ContactManagement.DAL.Entities;
using ContactManagement.Repo.Models;
using ContactManagement.Repo.Repositories;
using ContactManagement.Repo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.Repo.Services.Implementations
{
    public class EnterpriseService : IEnterpriseService
    {
        private readonly IEnterpriseRepository _enterpriseRepository;
        private ContactDBContext _dbContext;

        public EnterpriseService(IEnterpriseRepository enterpriseRepository, ContactDBContext dbContext)
        {

            _enterpriseRepository = enterpriseRepository;
        }

        public async Task<IEnumerable<Enterprise>> ListAsync()
        {
            return await _enterpriseRepository.GetAllAsync();
        }

        public async Task<Enterprise> GetByIdAsync(long id)
        {
            var item = await _enterpriseRepository.GetByIdAsync(id);
            return item;
        }


        public async Task<Enterprise> SaveAsync(EnterpriseDTOFull enterpriseDTO)
        {
            if (enterpriseDTO != null)
            {
                Enterprise enterprise = await _enterpriseRepository.GetByIdAsync(enterpriseDTO.Id);
                if (enterprise == null)
                    enterprise = new Enterprise();

                Mapper.Map<EnterpriseDTOFull, Enterprise>(enterpriseDTO, enterprise);

                enterprise.EnterpriseAdress = enterpriseDTO.Adresses.Select(x => new EnterpriseAdress()
                {
                    Adress = new Adress { 
                                Id = x.Id,
                                Name = x.Name,
                                City = x.City,
                                Country = x.Country,
                                PostalCode = x.PostalCode,
                                Street = x.Street,
                                StreetNumber = x.StreetNumber
                                },
                    EnterpriseId = enterprise.Id,
                    HeadOffice = x.HeadOffice
                }).ToList();

                await _enterpriseRepository.UpsertAsync(enterprise);

                return enterprise;
            }
            else
                throw new ArgumentNullException(nameof(enterpriseDTO));
        }

       public async Task DeleteAsync(long id)
        {
            var item = await _enterpriseRepository.GetByIdAsync(id);

            if (item == null) throw new NotFoundException(id);


            await _enterpriseRepository.DeleteAsync(item);
        }
    }
}
