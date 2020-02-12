using Helpdesk.Entities;
using System;
using System.Linq;
using System.Text;

namespace Helpdesk.Repository.Clients
{
    public interface IClientReadRepository
    {
        ClientEntity GetClientDetail(int clientId);
        IQueryable<ClientEntity> GetClients();
    }
}
