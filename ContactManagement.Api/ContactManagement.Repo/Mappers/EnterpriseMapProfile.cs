using AutoMapper;
using ContactManagement.DAL.Entities;
using ContactManagement.Repo.Models;


namespace ContactManagement.Repo.Mappers
{

    public class EnterpriseMapProfile : Profile
    {
        public EnterpriseMapProfile()
        {
            CreateMap<EnterpriseDTOBase, Enterprise>();
            CreateMap<Enterprise, EnterpriseDTOBase>();

            CreateMap<EnterpriseDTOFull, Enterprise>();
            CreateMap<Enterprise, EnterpriseDTOFull>();
        }
    }
}
