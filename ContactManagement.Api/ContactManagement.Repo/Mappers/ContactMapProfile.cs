using AutoMapper;
using ContactManagement.DAL.Entities;
using ContactManagement.Repo.Models;

namespace ContactManagement.Repo.Mappers
{

    public class ContactMapProfile : Profile
    {
        public ContactMapProfile()
        {
            CreateMap<ContactDTO, Contact>().ReverseMap();
        }
    }
}
