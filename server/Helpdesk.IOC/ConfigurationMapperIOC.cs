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

            services.AddScoped<IDataMapper<TeamModel, TeamEntity>, DataMapper<TeamModel, TeamEntity>>();
            services.AddScoped<IDataMapper<UserProfileModel, UserProfileEntity>, DataMapper<UserProfileModel, UserProfileEntity>>();

            services.AddScoped<IDataMapper<TicketListViewModel, TicketEntity>, DataMapper<TicketListViewModel, TicketEntity>>();
            services.AddScoped<IDataMapper<TicketDetailViewModel, TicketEntity>, DataMapper<TicketDetailViewModel, TicketEntity>>();

            services.AddScoped<IDataMapper<ClientDetailViewModel, ClientEntity>, DataMapper<ClientDetailViewModel, ClientEntity>>();
        }
    }
}
