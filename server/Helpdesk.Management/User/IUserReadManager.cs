using Helpdesk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpdesk.Management.User
{
    public interface IUserReadManager
    {
        UserProfileModel GetUser(int userId);
        IEnumerable<TeamModel> GetTeam(int teamId);
    }
}
