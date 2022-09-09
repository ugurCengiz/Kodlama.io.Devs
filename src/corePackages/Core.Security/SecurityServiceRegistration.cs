﻿using Core.Security.JWT;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Security.EmailAuthenticator;
using Core.Security.OtpAuthenticator;
using Core.Security.OtpAuthenticator.OtpNet;

namespace Core.Security
{
    public static class SecurityServiceRegistration
    {
        public static IServiceCollection AddSecurityServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHelper, JwtHelper>();
            services.AddScoped<IEmailAuthenticatorHelper, EmailAuthenticatorHelper>();
            services.AddScoped<IOtpAuthenticatorHelper, OtpNetOtpAuthenticatorHelper>();
            return services;
        }
    }
}
