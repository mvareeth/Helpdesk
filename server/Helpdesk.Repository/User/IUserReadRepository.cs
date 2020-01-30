using Helpdesk.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpdesk.Repository.User
{
    public interface IUserReadRepository
    {
        UserProfile GetUser(int userId);
        IQueryable<Team> GetTeam(int teamId);
    }
}
