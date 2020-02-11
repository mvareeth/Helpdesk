using Helpdesk.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Data.Map
{
    public class AccountMap : IMap
    {
        /// <summary>
        /// mapping the table with entity
        /// </summary>
        /// <param name="builder"></param>
        public void TableMap(ModelBuilder builder)
        {
            builder.Entity<Account>()
                .ToTable("Account")
                .HasKey(x => x.Id);
            InsertUserProfileRecord(builder);
        }

        /// <summary>
        /// insert initial team records
        /// </summary>
        /// <param name="builder"></param>
        private void InsertUserProfileRecord(ModelBuilder builder)
        {
            builder.Entity<Account>().HasData
            (
                new Account
                {
                    Id = 1001,
                    UserName = "Issac",
                    Password = "I100",
                    CreatedDate = DateTime.Now
                },
                new Account
                {
                    Id = 1002,
                    UserName = "Timothi",
                    Password = "I100",
                    CreatedDate = DateTime.Now
                },
                new Account
                {
                    Id = 1003,
                    UserName = "Kevin",
                    Password = "I100",
                    CreatedDate = DateTime.Now
                }
            );
        }
    }
}
