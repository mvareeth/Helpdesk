using Helpdesk.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Data
{
    public class TicketMap : IMap
    {
        /// <summary>
        /// mapping the table with entity
        /// </summary>
        /// <param name="builder"></param>
        public void TableMap(ModelBuilder builder)
        {
            builder.Entity<TicketEntity>()
                .ToTable("Ticket")
                .HasKey(x => x.Id);
            InsertTicketRecord(builder);
        }
        /// <summary>
        /// insert initial team records
        /// </summary>
        /// <param name="builder"></param>
        private void InsertTicketRecord(ModelBuilder builder)
        {
            builder.Entity<TicketEntity>().HasData
            (
                new TicketEntity
                {
                    Id = 3001,
                    ClientId = 201,
                    Title = "Fixing Air Valve",
                    Complexity = 1,
                    Priority = 1,
                    Notes = "Air leak",
                    Description = "Replace the air value if it is damaged",
                    AssigedTechnicianId = 1001,
                    CreatedDate = DateTime.Now,
                    CreatedBy = 1001,
                    StatusId = 2
                },
                new TicketEntity
                {
                    Id = 3002,
                    ClientId = 202,
                    Title = "Fixing Air Valve",
                    Complexity = 1,
                    Priority = 1,
                    Notes = "Air leak",
                    Description = "Replace the air value if it is damaged",
                    AssigedTechnicianId = 1002,
                    CreatedDate = DateTime.Now,
                    CreatedBy = 1001,
                    StatusId = 3
                },
                new TicketEntity
                {
                    Id = 3003,
                    ClientId = 202,
                    Title = "Fixing Air Valve",
                    Complexity = 1,
                    Priority = 1,
                    Notes = "Air leak",
                    Description = "Replace the air value if it is damaged",
                    AssigedTechnicianId = 1003,
                    CreatedDate = DateTime.Now,
                    CreatedBy = 1001,
                    StatusId = 3
                }
            );
        }
    }
}
