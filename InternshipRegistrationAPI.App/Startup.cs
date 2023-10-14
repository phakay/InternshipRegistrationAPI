using AutoMapper;
using InternshipRegistrationAPI.Data;
using InternshipRegistrationAPI.Data.Contracts;
using InternshipRegistrationAPI.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddScoped<IFormRepository, FormRepository>();

            services.AddScoped<IWorkflowRepository, WorkflowRepository>();

            services.AddScoped<IMapper>(svc => new MapperConfiguration(conf => conf.AddProfile<MappingProfile>()).CreateMapper());
        }
    }
}