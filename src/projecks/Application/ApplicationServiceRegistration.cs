using Application.Features.ProgrammingLanguages.Rules;
using Core.Application.Pipelines.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Features.Authorizations.Rules;
using Application.Features.Technologies.Rules;
using Application.Services.AuthService;
using Application.Services.UserService;
using Core.Application.Pipelines.Authorization;
using Core.Security.JWT;
using Microsoft.AspNetCore.Http;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());


            services.AddScoped<ProgrammingLanguageRules>();
            services.AddScoped<TechnologyBusinessRules>();
            services.AddScoped<AuthorizationsBusinessRules>();
          

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<IUserService, UserManager>();
            services.AddTransient<TokenOptions>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }
    }
}
