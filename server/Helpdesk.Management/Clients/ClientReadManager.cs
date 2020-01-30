using Helpdesk.DataMapper;
using Helpdesk.Model;
using Helpdesk.Repository.Clients;
using Helpdesk.Entities;

namespace Helpdesk.Management.Clients
{
    public class ClientReadManager: IClientReadManager
    {

        private readonly IClientReadRepository clientReadRepository;
        private readonly IDataMapper<ClientDetailModel, Client> clientMapper;
        public ClientReadManager(IClientReadRepository clientReadRepository, IDataMapper<ClientDetailModel, Client> clientMapper)
        {
            this.clientReadRepository = clientReadRepository;
            this.clientMapper = clientMapper;
        }
        public ClientDetailModel GetClientDetail(int clientId)
        {
            var clientDetail = clientReadRepository.GetClientDetail(clientId);
            var clientDetailModel = clientMapper.EntityToModel(clientDetail);
            return clientDetailModel;
        }
    }
}
