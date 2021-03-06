﻿using Helpdesk.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Data
{
    public class ClientMap: IMap
    {
        /// <summary>
        /// mapping the table with entity
        /// </summary>
        /// <param name="builder"></param>
        public void TableMap(ModelBuilder builder)
        {
            builder.Entity<ClientEntity>()
                .ToTable("Client")
                .HasKey(x => x.Id);

            this.InsertClientRecord(builder);
        }

        /// <summary>
        /// insert initial team records
        /// </summary>
        /// <param name="builder"></param>
        private void InsertClientRecord(ModelBuilder builder)
        {
            builder.Entity<ClientEntity>().HasData
            (
                new ClientEntity
                {
                    Id = 201,
                    FirstName = "Abraham",
                    LastName = "Thomas",
                    Company = "Delta Airlines",
                    CreatedDate = DateTime.Now
                },
                new ClientEntity
                {
                    Id = 202,
                    FirstName = "Jeevan",
                    LastName = "Puthen",
                    Company = "American Airlines",
                    CreatedDate = DateTime.Now
                }
            );
        }
    }

}
