using Helpdesk.Data;
using Helpdesk.Entities;
using System.Linq;

namespace Helpdesk.Repository.Clients
{
    public class ClientReadRepository: IClientReadRepository
    {
        private readonly IUnitOfWork uow;

        public ClientReadRepository(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public ClientEntity GetClientDetail(int clientId)
        {
            return uow.Query<ClientEntity>()
                .Where(x => x.Id == clientId).FirstOrDefault();
        }
        public IQueryable<ClientEntity> GetClients()
        {
            return uow.Query<ClientEntity>();
        }
    }
}
