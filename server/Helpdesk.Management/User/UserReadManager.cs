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
        private readonly IDataMapper<TeamModel, Team> teamMapper;
        private readonly IDataMapper<UserProfileModel, UserProfile> userProfileMapper;
        public UserReadManager(IUserReadRepository userReadRepository, IDataMapper<TeamModel, Team> teamMapper, IDataMapper<UserProfileModel, UserProfile> userProfileMapper)
        {
            this.userReadRepository = userReadRepository;
            this.teamMapper = teamMapper;
            this.userProfileMapper = userProfileMapper;
        }
        public IEnumerable<TeamModel> GetTeam(int teamId)
        {
            var team = userReadRepository.GetTeam(teamId);
            var teamModel = teamMapper.EntityToModel(team);
            return teamModel;
        }

        public UserProfileModel GetUser(int userId)
        {
            var userProfile = userReadRepository.GetUser(userId);
            var userProfileModel = userProfileMapper.EntityToModel(userProfile);
            return userProfileModel;
        }
    }
}
