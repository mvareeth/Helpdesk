using Helpdesk.Cache;
using Helpdesk.Data;
using Helpdesk.Security.Token;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Helpdesk.IOC
{
    public partial class IOCConfiguration : IIOCConfiguration
    {
        public void Configure(IServiceCollection services)
        {
            this.ConfigureContextObject(services);
            this.ConfigurePermissionObject(services);
            this.ConfigureCrossCutting(services);
            this.ConfigureMapper(services);
            this.ConfigureManagement(services);
            this.ConfigureRepository(services);
        }

        private void ConfigureContextObject(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
        }
        private void ConfigurePermissionObject(IServiceCollection services)
        {
            services.AddScoped<ITokenProvider, TokenProvider>();
        }
        public void ConfigureCrossCutting(IServiceCollection services)
        {
            services.AddScoped<ICache, MemCache>();  
        }
    }
}
