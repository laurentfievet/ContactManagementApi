using AutoMapper;
using ContactManagement.DAL.Entities;
using ContactManagement.Repo.Models;


namespace ContactManagement.Repo.Mappers
{

    public class AdressMapProfile : Profile
    {
        public AdressMapProfile()
        {
            CreateMap<AdressDTO, Adress>();
            CreateMap<Adress, AdressDTO>();
        }
    }
}
