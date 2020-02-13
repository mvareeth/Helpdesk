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
        /// <summary>
        /// get a particular user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserProfileEntity GetUser(int userId)
        {
            return uow.Query<UserProfileEntity>()
                .Where(x => x.Id == userId).FirstOrDefault();
        }
        /// <summary>
        /// get list of teams
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public IQueryable<TeamEntity> GetTeam(int teamId)
        {
            return uow.Query<TeamEntity>()
                .Where(x => x.TeamId == teamId);
        }
        /// <summary>
        /// get users from storage
        /// </summary>
        /// <returns></returns>
        public IQueryable<UserProfileEntity> GetUsers()
        {
            return uow.Query<UserProfileEntity>();
        }
    }
}
