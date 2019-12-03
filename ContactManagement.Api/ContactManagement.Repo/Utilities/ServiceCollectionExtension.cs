using ContactManagement.DAL;
using ContactManagement.Repo.Repositories;
using ContactManagement.Repo.Services;
using ContactManagement.Repo.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagement.Repo.Utilities
{
    public static class ServiceCollectionExtension
    {
        public static void AddEntityFramework(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ContactDBContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("ContactManagementDB"));
               
            });


        }

        public static void AddSwaggerService(this IServiceCollection services, string assemblyName)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Contact Management API",
                    Version = "V1",
                    Description = "API for Core Version"
                });
                options.IncludeXmlComments(string.Format(@"{0}\{1}.XML", System.AppDomain.CurrentDomain.BaseDirectory, assemblyName));
            });
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IEnterpriseRepository, EnterpriseRepository>();
            services.AddScoped<IAdressRepository, AdressRepository>();
        }
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IEnterpriseService, EnterpriseService>();
        }

    }
}
