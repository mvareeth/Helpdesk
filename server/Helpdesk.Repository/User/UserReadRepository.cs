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

        public UserProfileEntity GetUser(int userId)
        {
            return uow.Query<UserProfileEntity>()
                .Where(x => x.Id == userId).FirstOrDefault();
        }
        public IQueryable<TeamEntity> GetTeam(int teamId)
        {
            return uow.Query<TeamEntity>()
                .Where(x => x.TeamId == teamId);
        }
    }
}
