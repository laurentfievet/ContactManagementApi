using AutoMapper;


namespace ContactManagement.Repo.Mappers
{
    public static class MapperConfigurator
    {
        public static void Configure()
        {

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ContactMapProfile>();
                cfg.AddProfile<AdressMapProfile>();
                cfg.AddProfile<EnterpriseMapProfile>();
            });

        }
    }
}
