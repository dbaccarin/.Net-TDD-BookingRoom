using Microsoft.EntityFrameworkCore;
using RoomBookingApp.Domain;
using RoomBookingApp.Persistence.Repositories;

namespace RoomBookingApp.Persistence.Tests
{
    public class RoomBookingServiceTest
    {
        [Fact]
        public void Should_Return_Available_Rooms()
        {
            //Arrange
            var date = new DateTime(2025, 6, 19);

            var dbOptions = new DbContextOptionsBuilder<RoomBookingAppDbContext>().UseInMemoryDatabase("AvailableRoomTest").Options;

            using var context = new RoomBookingAppDbContext(dbOptions);

            context.Rooms.Add(new Room { Id = 1, Name = "Room 1" });
            context.Rooms.Add(new Room { Id = 2, Name = "Room 2" });
            context.Rooms.Add(new Room { Id = 3, Name = "Room 3" });

            context.RoomBookings.Add(new RoomBooking { Id = 1, Date = date });
            context.RoomBookings.Add(new RoomBooking { Id = 2, Date = date.AddDays(-1) });

            context.SaveChanges();

            var roomBookingService = new RoomBookingService(context);

            //Act
            var availableRooms = roomBookingService.GetAvailableRooms(date);

            //Assert
            Assert.Equal(2, availableRooms.Count());
            Assert.DoesNotContain(availableRooms, q => q.Id == 1);
            Assert.Contains(availableRooms, q => q.Id == 2);
            Assert.Contains(availableRooms, q => q.Id == 3);
        }
    }
}