using Helpdesk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpdesk.Management.User
{
    public interface IUserReadManager
    {
        /// <summary>
        /// Get the user profile based on the user id
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>user profile</returns>
        UserProfileModel GetUser(int userId);
        /// <summary>
        /// Get the team based on the team id
        /// </summary>
        /// <param name="teamId">team id</param>
        /// <returns>team list</returns>
        IEnumerable<TeamModel> GetTeam(int teamId);
    }
}
