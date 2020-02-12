using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helpdesk.DataMapper;
using Helpdesk.Entities;
using Helpdesk.Model;
using Helpdesk.Repository.User;

namespace Helpdesk.Management.User
{
    public class UserReadManager: IUserReadManager
    {
        private readonly IUserReadRepository userReadRepository;
        private readonly IDataMapper<TeamModel, TeamEntity> teamMapper;
        private readonly IDataMapper<UserProfileModel, UserProfileEntity> userProfileMapper;
        public UserReadManager(IUserReadRepository userReadRepository, IDataMapper<TeamModel, TeamEntity> teamMapper, IDataMapper<UserProfileModel, UserProfileEntity> userProfileMapper)
        {
            this.userReadRepository = userReadRepository;
            this.teamMapper = teamMapper;
            this.userProfileMapper = userProfileMapper;
        }
        /// <summary>
        /// Get the team based on the team id
        /// </summary>
        /// <param name="teamId">team id</param>
        /// <returns>team list</returns>
        public IEnumerable<TeamModel> GetTeam(int teamId)
        {
            var team = userReadRepository.GetTeam(teamId);
            var teamModel = teamMapper.EntityToModel(team);
            return teamModel;
        }
        /// <summary>
        /// Get the user profile based on the user id
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>user profile</returns>
        public UserProfileModel GetUser(int userId)
        {
            var userProfile = userReadRepository.GetUser(userId);
            var userProfileModel = userProfileMapper.EntityToModel(userProfile);
            return userProfileModel;
        }
    }
}
