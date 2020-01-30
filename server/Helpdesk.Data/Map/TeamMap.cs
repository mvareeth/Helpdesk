using Helpdesk.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Data
{
    
    public class TeamMap : IMap
    {
        /// <summary>
        /// mapping the table with entity
        /// </summary>
        /// <param name="builder"></param>
        public void TableMap(ModelBuilder builder)
        {
            builder.Entity<Team>()
                .ToTable("Team")
                .HasKey(x => x.Id);

            this.InsertTeamRecord(builder);
        }
        /// <summary>
        /// insert initial team records
        /// </summary>
        /// <param name="builder"></param>
        private void InsertTeamRecord(ModelBuilder builder)
        {
            builder.Entity<Team>().HasData
            (
                new Team
                {
                    Id = 5001,
                    TeamId = 100,
                    UserId = 1001,
                    IsTeamLead = false,
                    TeamName = "Helpdesk"
                },
                new Team
                {
                    Id = 5002,
                    TeamId = 100,
                    UserId = 1002,
                    IsTeamLead = false,
                    TeamName = "Helpdesk"
                },
                new Team
                {
                    Id = 5003,
                    TeamId = 100,
                    UserId = 1003,
                    IsTeamLead = true,
                    TeamName = "Helpdesk"
                }
            );
        }
    }
}
