using Helpdesk.Repository.Clients;
using Helpdesk.Repository.Tickets;
using Helpdesk.Repository.User;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.IOC
{
    public partial class IOCConfiguration
    {
        protected void ConfigureRepository(IServiceCollection services)
        {
            this.ConfigureUserRepository(services);
            this.ConfigureTicketRepository(services);
            this.ConfigureClientRepository(services);
        }

        private void ConfigureTicketRepository(IServiceCollection services)
        {
            services.AddScoped<ITicketWriteRepository, TicketWriteRepository>();
            services.AddScoped<ITicketReadRepository, TicketReadRepository>();
        }

        private void ConfigureUserRepository(IServiceCollection services)
        {
            services.AddScoped<IUserReadRepository, UserReadRepository>();
        }

        private void ConfigureClientRepository(IServiceCollection services)
        {
            services.AddScoped<IClientReadRepository, ClientReadRepository>();
        }
    }
}
