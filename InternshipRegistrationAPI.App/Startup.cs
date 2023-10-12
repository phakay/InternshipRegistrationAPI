﻿using AutoMapper;
using InternshipRegistrationAPI.Data;
using InternshipRegistrationAPI.Data.Contracts;
using InternshipRegistrationAPI.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace InternshipRegistrationAPI.App
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(conf => conf.MapControllers());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IApplicationDbContext, ApplicationDbContext>();

            services.AddScoped<IProgramRepository, ProgramRepository>();

            services.AddSingleton<IMapper>(svc =>
            {
                var mappingConfig = new MapperConfiguration(conf =>
                {
                    conf.AddProfile<MappingProfile>();
                });
                return mappingConfig.CreateMapper();
            });
        }
    }
}