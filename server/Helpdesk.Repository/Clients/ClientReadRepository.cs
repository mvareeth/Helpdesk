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

        public Client GetClientDetail(int clientId)
        {
            return uow.Query<Client>()
                .Where(x => x.Id == clientId).FirstOrDefault();
        }
    }
}
