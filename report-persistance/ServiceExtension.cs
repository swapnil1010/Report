using Microsoft.Extensions.DependencyInjection;
using report_core.Domain.Interfaces;
using report_persistance.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using report_core.Application.Services.Interfaces;
using report_core.Domain.Entities.Common;
using report_core.Domain.Interfaces.Project;
using report_persistance.Implementations.Repositories.Project;
using report_core.Application.Services.Implementations.Project;
using report_core.Application.Services.Implementations.Login;
using report_core.Domain.Interfaces.Login;
using report_persistance.Implementations.Repositories.Login;

namespace report_persistance
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ILoginService, LoginService>();  
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            return services;
        }
    }
}
