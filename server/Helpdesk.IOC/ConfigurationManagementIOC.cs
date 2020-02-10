using Helpdesk.IOC;
using Helpdesk.Management.Clients;
using Helpdesk.Management.Security;
using Helpdesk.Management.Tickets;
using Helpdesk.Management.User;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.IOC
{
    public partial class IOCConfiguration  
    {
        protected void ConfigureManagement(IServiceCollection services)
        {
            this.ConfigureUserManager(services);
            this.ConfigureTicketManager(services);
            this.ConfigureClientManager(services);
        }

        private void ConfigureTicketManager(IServiceCollection services)
        {
            services.AddScoped<ITicketWriteManager, TicketWriteManager>();
            services.AddScoped<ITicketReadManager, TicketReadManager>();
        }

        private void ConfigureUserManager(IServiceCollection services)
        {
            services.AddScoped<IUserReadManager, UserReadManager>();
            services.AddScoped<ISecurityReadManager, SecurityReadManager>();
        }

        private void ConfigureClientManager(IServiceCollection services)
        {
            services.AddScoped<IClientReadManager, ClientReadManager>();
        }
    }
}
