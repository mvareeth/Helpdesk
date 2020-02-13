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
        /// <summary>
        /// get the account based on user name and password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int? GetAccountId(string userName, string password)
		{
            return uow.Query<AccountEntity>()
                .Where(x => x.UserName.ToLower() == userName.ToLower() && x.Password == password ).FirstOrDefault()?.Id;
        }
	}
}
