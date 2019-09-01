using Desafio.CrossCutting.Helpers;
using Desafio.Domain.Contracts;
using Desafio.Domain.Entities;
using Desafio.Repositories;
using Desafio.Repositories.Repositories;
using Desafio.Services.Services;
using Desafio.Services.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Desafio.CrossCutting.IoC
{
    public class BootStrapper
    {
        public static void Register(IServiceCollection services)
        {
            RegisterContext(services);
            RegisterServices(services);
            RegisterRepositories(services);
        }

        private static void RegisterContext(IServiceCollection services)
        {
            services.AddDbContext<DesafioDbContext>(options => options.UseInMemoryDatabase(databaseName: "DesafioDbPitang"));
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IValidator<User>, UserValidator>();
            services.AddTransient<IValidator<User>, LoginValidator>();
            services.AddTransient<IValidator<Phone>, PhoneValidator>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
