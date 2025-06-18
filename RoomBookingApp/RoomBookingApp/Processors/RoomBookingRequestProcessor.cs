using RoomBookingApp.DataServices;
using RoomBookingApp.Domain;
using RoomBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookingApp.Processors
{
    public class RoomBookingRequestProcessor
    {
        private readonly IRoomBookingService _roomBookingService;

        public RoomBookingRequestProcessor(IRoomBookingService roomBookingService)
        {
            _roomBookingService = roomBookingService;
        }

        public RoomBookingResult BookRoom(RoomBookingRequest request)
        {

            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var availableRooms = _roomBookingService.GetAvailableRooms(request.Date);

            if (availableRooms.Any())
            {
                var room = availableRooms.First();
                var roomBooking = CreateRoomBookingObject<RoomBooking>(request);
                roomBooking.RoomId = room.Id;

                _roomBookingService.Save(roomBooking);

            }

            return CreateRoomBookingObject<RoomBookingResult>(request);
        }

        private static T CreateRoomBookingObject<T>(RoomBookingRequest request) where T : RoomBookingBase, new()
        {
            return new T
            {
                FullName = request.FullName,
                Date = request.Date,
                Email = request.Email
            };
        }

    }
}
