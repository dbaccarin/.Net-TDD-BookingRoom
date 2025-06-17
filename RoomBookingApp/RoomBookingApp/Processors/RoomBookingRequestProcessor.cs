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

            _roomBookingService.Save(new RoomBooking() {
                FullName = request.FullName,
                Date = request.Date,
                Email = request.Email
            });

            return new RoomBookingResult
            {
                FullName = request.FullName,
                Date = request.Date,
                Email = request.Email
            };
        }
    }
}
