﻿using Microsoft.EntityFrameworkCore;
using PassIn.Infrastructure.Entities;

namespace PassIn.Infrastructure
{
    public class PassInDbContent : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Attendees> Attendees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Users\\Pc\\Documents\\PassInDb (1).db");
        }
    }
}