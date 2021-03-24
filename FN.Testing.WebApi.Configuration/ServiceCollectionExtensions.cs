using FluentValidation;
using FluentValidation.Application.Validators;
using FN.Testing.Application.Contract.Models;
using FN.Testing.Application.Contract.Services;
using FN.Testing.Application.Mapping;
using FN.Testing.Application.Services;
using FN.Testing.Business.Contract.Abstractions;
using FN.Testing.Business.Mapping;
using FN.Testing.Business.Services;
using FN.Testing.DataLayer.Contract.Abstractions;
using FN.Testing.DataLayer.Contract.Tables;
using FN.Testing.DataLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FN.Testing.WebApi.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUploadService, UploadService>();
            serviceCollection.AddScoped<IValidator<UploadModel>, UploadModelValidator>();
            serviceCollection.AddAutoMapper((srv, cfg) =>
            {
                cfg.AddProfile(typeof(ApplicationMappingProfile));
            },
            new Assembly[0], ServiceLifetime.Transient);
            return serviceCollection;
        }
        public static IServiceCollection AddBusinessServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUploadDataService, UploadDataService>();
            serviceCollection.AddAutoMapper((srv, cfg) =>
            {
                cfg.AddProfile(typeof(BusinessMappingProfile));
            },
            new Assembly[0], ServiceLifetime.Transient);
            return serviceCollection;
        }
        public static IServiceCollection AddDataLayer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUploadRepository, UploadRepository>();
            return serviceCollection;
        }
    }
}
