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
        /// <summary>
        /// get a client details based on clientid
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public ClientEntity GetClientDetail(int clientId)
        {
            return uow.Query<ClientEntity>()
                .Where(x => x.Id == clientId).FirstOrDefault();
        }
        /// <summary>
        /// get all client list
        /// </summary>
        /// <returns></returns>
        public IQueryable<ClientEntity> GetClients()
        {
            return uow.Query<ClientEntity>();
        }
    }
}
