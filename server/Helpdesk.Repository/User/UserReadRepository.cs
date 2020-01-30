using Helpdesk.Data;
using Helpdesk.Entities;
using System.Linq;

namespace Helpdesk.Repository.User
{
    public class UserReadRepository: IUserReadRepository
    {
        private readonly IUnitOfWork uow;

        public UserReadRepository(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public UserProfile GetUser(int userId)
        {
            return uow.Query<UserProfile>()
                .Where(x => x.Id == userId).FirstOrDefault();
        }
        public IQueryable<Team> GetTeam(int teamId)
        {
            return uow.Query<Team>()
                .Where(x => x.TeamId == teamId);
        }
    }
}
