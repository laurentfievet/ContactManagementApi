using AutoMapper;
using ContactManagement.DAL.Entities;
using ContactManagement.Repo.Models;


namespace ContactManagement.Repo.Mappers
{

    public class EnterpriseMapProfile : Profile
    {
        public EnterpriseMapProfile()
        {
            CreateMap<EnterpriseDTOBase, Enterprise>().ReverseMap();
            CreateMap<EnterpriseDTOFull, Enterprise>().ReverseMap();

        }
    }
}
