﻿
using Microsoft.EntityFrameworkCore;
using RoomBookingApp.Domain;

namespace RoomBookingApp.Persistence
{
    public class RoomBookingAppDbContext : DbContext
    {
        public RoomBookingAppDbContext(DbContextOptions<RoomBookingAppDbContext> options) : base(options)
        {

        }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomBooking> RoomBookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Name = "Conference Room A" },
                new Room { Id = 1, Name = "Conference Room A" },
                new Room { Id = 1, Name = "Conference Room A" }
                );
        }

    }
}
