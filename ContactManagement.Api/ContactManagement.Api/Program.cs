using ContactManagement.Repo.Mappers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace ContactManagement.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MapperConfigurator.Configure();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
