using AutoMapper;
using ContactManagement.DAL.Entities;
using ContactManagement.Repo.Models;
using System.Linq;

namespace ContactManagement.Repo.Mappers
{

    public class ContactMapProfile : Profile
    {
        public ContactMapProfile()
        {
            CreateMap<ContactDTO, Contact>();
               

            CreateMap<Contact, ContactDTO>();
        }
    }
}
