using Helpdesk.Data;
using Helpdesk.Entities;
using System.Linq;


namespace Helpdesk.Repository.Security
{
	public class SecurityReadRepository : ISecurityReadRepository
	{
        private readonly IUnitOfWork uow;

        public SecurityReadRepository(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public int? GetAccountId(string userName, string password)
		{
            return uow.Query<Account>()
                .Where(x => x.UserName == userName && x.Password == password ).FirstOrDefault()?.Id;
        }
	}
}
