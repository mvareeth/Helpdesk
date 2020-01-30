using Helpdesk.Data;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Helpdesk.IOC
{
    public partial class IOCConfiguration : IIOCConfiguration
    {
        public void Configure(IServiceCollection services)
        {
            this.ConfigureContextObject(services);
            this.ConfigureMapper(services);
            this.ConfigureManagement(services);
            this.ConfigureRepository(services);
        }

        private void ConfigureContextObject(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
        }
    }
}
