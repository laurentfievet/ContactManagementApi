using AutoMapper;
using ContactManagement.DAL;
using ContactManagement.DAL.Entities;
using ContactManagement.Repo.Models;
using ContactManagement.Repo.Repositories;
using ContactManagement.Repo.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagement.Repo.Services.Implementations
{
    public class EnterpriseService : IEnterpriseService
    {
        private readonly IEnterpriseRepository _enterpriseRepository;

        public EnterpriseService(IEnterpriseRepository enterpriseRepository, ContactDBContext dbContext)
        {

            _enterpriseRepository = enterpriseRepository;
        }

        public async Task<IEnumerable<Enterprise>> ListAsync()
        {
            return await _enterpriseRepository.GetAllAsync();
        }

        public async Task<EnterpriseDTOFull> GetByIdAsync(long id)
        {
            var item = await _enterpriseRepository.GetByIdAsync(id);
            return item;
        }


        public async Task<Enterprise> SaveAsync(EnterpriseDTOFull enterpriseDTO)
        {
            if (enterpriseDTO != null)
            {
                Enterprise enterprise = await _enterpriseRepository.FindByIdAsync(enterpriseDTO.Id);
                if (enterprise == null)
                    enterprise = new Enterprise();

                Mapper.Map<EnterpriseDTOFull, Enterprise>(enterpriseDTO, enterprise);

                enterprise.EnterpriseAddress = enterpriseDTO.Addresses.Select(x => new EnterpriseAddress()
                {
                    Address = new Address { 
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

        public async Task<Enterprise> AddAddressesAsync(long enterpriseId, List<EnterpriseAddressDTO> enterpriseDTOList)
        {
           
                Enterprise enterprise = await _enterpriseRepository.FindByIdAsync(enterpriseId);
                if (enterprise == null)
                    throw new ArgumentNullException(nameof(enterprise));

                    bool newHeadOffice = enterpriseDTOList.Where(x => x.HeadOffice == true).Count() > 0;

                    if (newHeadOffice)
                    {
                        enterprise.EnterpriseAddress.Where(x => x.HeadOffice == true).FirstOrDefault().HeadOffice = false; 
                    }
                    
                    foreach (EnterpriseAddressDTO dto in enterpriseDTOList)
                    {
                        enterprise.EnterpriseAddress.Add(new EnterpriseAddress
                        {
                            Address = new Address { 
                                Id = dto.Id,
                                City = dto.City,
                                Country = dto.Country,
                                Name = dto.Name,
                                PostalCode = dto.PostalCode,
                                Street = dto.Street,
                                StreetNumber = dto.StreetNumber
                            },
                            EnterpriseId = enterpriseId,
                            HeadOffice = dto.HeadOffice
                        });
                    }

                await _enterpriseRepository.UpsertAsync(enterprise);

                return enterprise;
           
        }

        public async Task DeleteAsync(long id)
        {
            var item = await _enterpriseRepository.FindByIdAsync(id);

            if (item == null) throw new NotFoundException(id);


            await _enterpriseRepository.DeleteAsync(item);
        }
    }
}
