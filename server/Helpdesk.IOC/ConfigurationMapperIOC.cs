using Helpdesk.DataMapper;
using Helpdesk.Entities;
using Helpdesk.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.IOC
{
    public partial class IOCConfiguration
    {
        protected void ConfigureMapper(IServiceCollection services)
        {

            services.AddScoped<IDataMapper<TeamModel, Team>, DataMapper<TeamModel, Team>>();
            services.AddScoped<IDataMapper<UserProfileModel, UserProfile>, DataMapper<UserProfileModel, UserProfile>>();

            services.AddScoped<IDataMapper<TicketListModel, Ticket>, DataMapper<TicketListModel, Ticket>>();
            services.AddScoped<IDataMapper<TicketDetailModel, Ticket>, DataMapper<TicketDetailModel, Ticket>>();

            services.AddScoped<IDataMapper<ClientDetailModel, Client>, DataMapper<ClientDetailModel, Client>>();
        }
    }
}
