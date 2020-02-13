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
        /// <summary>
        /// get the details of a client based on the clientid
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns>client full information</returns>
        public ClientDetailViewModel GetClientDetail(int clientId)
        {
            var clientDetail = clientReadRepository.GetClientDetail(clientId);
            var clientDetailModel = clientMapper.EntityToModel(clientDetail);
            return clientDetailModel;
        }
        /// <summary>
        /// Get the list of clients
        /// </summary>
        /// <returns>client list</returns>
        public IEnumerable<ClientDetailViewModel> GetClients()
        {
            var clientList = clientReadRepository.GetClients();
            var clientListModel = clientMapper.EntityToModel(clientList);
            return clientListModel;
        }
    }
}
