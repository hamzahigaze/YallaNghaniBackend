using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Services.Account;
using YallaNghani.Services.Auth;
using YallaNghani.Services.JWTManager;
using YallaNghani.Services.OfferedCourses;
using YallaNghani.Services.Parents;
using YallaNghani.Services.Teachers;

namespace YallaNghani.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IParentsService, ParentsService>();
            services.AddScoped<ITeachersService, TeachersService>();
            services.AddScoped<IJWTManagerService, JWTManagerService>();
            services.AddScoped<IAccountsService, AccountsService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IOfferedCoursesService, OfferedCoursesService>();
            return services;
        }
    }
}
