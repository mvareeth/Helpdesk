using Helpdesk.DataMapper;
using Helpdesk.Model;
using Helpdesk.Repository.Clients;
using Helpdesk.Entities;
using System.Collections.Generic;

namespace Helpdesk.Management.Clients
{
    public class ClientReadManager: IClientReadManager
    {

        private readonly IClientReadRepository clientReadRepository;
        private readonly IDataMapper<ClientDetailViewModel, ClientEntity> clientMapper;
        public ClientReadManager(IClientReadRepository clientReadRepository, IDataMapper<ClientDetailViewModel, ClientEntity> clientMapper)
        {
            this.clientReadRepository = clientReadRepository;
            this.clientMapper = clientMapper;
        }
        public ClientDetailViewModel GetClientDetail(int clientId)
        {
            var clientDetail = clientReadRepository.GetClientDetail(clientId);
            var clientDetailModel = clientMapper.EntityToModel(clientDetail);
            return clientDetailModel;
        }
        public IEnumerable<ClientDetailViewModel> GetClients()
        {
            var clientList = clientReadRepository.GetClients();
            var clientListModel = clientMapper.EntityToModel(clientList);
            return clientListModel;
        }
    }
}
