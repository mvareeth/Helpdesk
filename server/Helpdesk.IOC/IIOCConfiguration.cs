using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.IOC
{
    public interface IIOCConfiguration
    {
        void Configure(IServiceCollection services);
    }
}
