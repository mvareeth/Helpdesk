using Helpdesk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpdesk.Repository.User
{
    public interface IUserReadRepository
    {
        UserProfileEntity GetUser(int userId);
        IQueryable<TeamEntity> GetTeam(int teamId);
    }
}
