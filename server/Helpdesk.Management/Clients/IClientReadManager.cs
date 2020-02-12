using Helpdesk.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Management.Clients
{
    public interface IClientReadManager
    {
        ClientDetailViewModel GetClientDetail(int clientId);
        IEnumerable<ClientDetailViewModel> GetClients();
	}
}
