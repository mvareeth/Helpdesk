using Helpdesk.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Data
{
    public class UserProfileMap : IMap
    {     
        /// <summary>
        /// mapping the table with entity
        /// </summary>
        /// <param name="builder"></param>
        public void TableMap(ModelBuilder builder)
        {
            builder.Entity<UserProfileEntity>()
                .ToTable("UserProfile")
                .HasKey(x => x.Id);
            InsertUserProfileRecord(builder);
        }

        /// <summary>
        /// insert initial team records
        /// </summary>
        /// <param name="builder"></param>
        private void InsertUserProfileRecord(ModelBuilder builder)
        {
            builder.Entity<UserProfileEntity>().HasData
            (
                new UserProfileEntity
                {
                    Id = 1001,
                    FirstName = "Issac",
                    LastName = "Alias",
                    CreatedDate = DateTime.Now
                },
                new UserProfileEntity
                {
                    Id = 1002,
                    FirstName = "Timothi",
                    LastName = "Joseph",
                    CreatedDate = DateTime.Now
                },
                new UserProfileEntity
                {
                    Id = 1003,
                    FirstName = "Kevin",
                    LastName = "Jose",
                    CreatedDate = DateTime.Now
                }
            );
        }
    }
}
