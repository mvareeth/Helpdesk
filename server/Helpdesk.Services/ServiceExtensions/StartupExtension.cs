using Helpdesk.IOC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helpdesk.Services
{
    public static class StartupExtension
    {
        private static IOCConfiguration CIOC => new IOCConfiguration();
        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            CIOC.Configure(services);
        }
    }
}
