
using Excellerent.APIModularization;
using Excellerent.Usermanagement.Domain.Interfaces.RepositoryInterfaces;
using Excellerent.Usermanagement.Domain.Interfaces.ServiceInterfaces;
using Excellerent.Usermanagement.Domain.Services;
using Excellerent.Usermanagement.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Excellerent.UserManagement.Presentation
{
    public class UserManagementModuleRegistration : ModuleStartup
    {
        public override void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(UserManagementProfile));
            services.AddScoped<IGroupSetRepository, GroupSetRepository>();
            services.AddScoped<IGroupSetService, GroupSetService>();

            //////////////////////////////////////////////////////////
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IPermissionService, PermissionService>();

            services.AddScoped<IGroupSetPermissionRepository, GroupSetPermissionRepository>();
            services.AddScoped<IGroupSetPermissionService, GroupSetPermissionService>();  

        }
    }
}