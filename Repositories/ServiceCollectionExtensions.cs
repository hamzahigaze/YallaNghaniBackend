using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNghani.Repositories.Accounts;
using YallaNghani.Repositories.OfferedCourses;
using YallaNghani.Repositories.Parent;
using YallaNghani.Repositories.Teacher;

namespace YallaNghani.Repositories
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IParentsRepository, ParentsRepository>();
            services.AddScoped<ITeachersRepository, TeachersRepository>();
            services.AddScoped<IAccountsRepository, AccountsRepository>();
            services.AddScoped<IOfferedCoursesRepository, OfferedCoursesRepository>();
            return services;
        }
    }
}
