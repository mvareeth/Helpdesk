using Helpdesk.IOC;
using Helpdesk.Security.Token;
using Helpdesk.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Helpdesk.Services
{
    public static class StartupExtension
    {

        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            CIOC.Configure(services);
        }
        public static void ConfigureCors(this IServiceCollection services, string allowSpecificOrigin)
        {
            services.AddCors(options => {
                options.AddPolicy(allowSpecificOrigin,
                    builder => {
                        builder.WithOrigins(ServingURLs)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });
        }
        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            services.Configure<TokenProviderOptions>(options => {
                options.Issuer = Issuer;
                options.Audience = Audience;
                options.ValidFor = ValidForMinutes;
                options.SigningCredentials = new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddAuthentication(authOptions => {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SigningKey,
                    ValidateIssuer = true,
                    ValidIssuer = Issuer,
                    ValidateAudience = true,
                    ValidAudience = Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }


        #region Getters

        private static IConfigurationSection TokenProviderSection => Config.GetSection(nameof(TokenProviderOptions));

        private static IConfigurationSection AppSettingsSection => Config.GetSection(nameof(AppSettings));

        private static IOCConfiguration CIOC => new IOCConfiguration();
        public static IConfiguration Config { get; set; }

        private static string Issuer => TokenProviderSection[nameof(TokenProviderOptions.Issuer)];

        private static string Audience => TokenProviderSection[nameof(TokenProviderOptions.Audience)];

        private static string Path => TokenProviderSection[nameof(TokenProviderOptions.Path)];

        private static TimeSpan ValidForMinutes
        {
            get
            {
                int validFor = 0;
                int.TryParse(TokenProviderSection[nameof(TokenProviderOptions.ValidFor)], out validFor);
                return TimeSpan.FromMinutes(validFor);
            }
        }

        private static string SecretKey => "my$up3rs3cr3t_s3cr3tk3y!123";

        private static SymmetricSecurityKey SigningKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        private static string[] ServingURLs
        {
            get
            {
                string[] servingURLs = AppSettingsSection[nameof(AppSettings.ClientURLs)].Split(',');
                for (int index = 0; index < servingURLs.Length; index++)
                {
                    Uri clientUri;
                    if (Uri.TryCreate(servingURLs[index], UriKind.Absolute, out clientUri))
                    {
                        servingURLs[index] = string.Format("{0}://{1}", clientUri.Scheme, clientUri.Authority);
                    }
                    else
                    {
                        servingURLs[index] = servingURLs[index].TrimEnd('/');
                    }
                }

                return servingURLs;
            }
        }

        #endregion
    }
}
