using Helpdesk.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Repository.Clients
{
    public interface IClientReadRepository
    {
        Client GetClientDetail(int clientId);
    }
}
