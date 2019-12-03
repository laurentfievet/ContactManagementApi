using AutoMapper;
using ContactManagement.DAL.Entities;
using ContactManagement.Repo.Models;


namespace ContactManagement.Repo.Mappers
{

    public class AddressMapProfile : Profile
    {
        public AddressMapProfile()
        {
            CreateMap<AddressDTO, Address>().ReverseMap();
      
        }
    }
}
