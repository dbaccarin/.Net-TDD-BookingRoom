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

        public RoomBookingRequestProcessor(DataServices.IRoomBookingService @object)
        {
        }

        public RoomBookingResult BookRoom(RoomBookingRequest request)
        {

            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return new RoomBookingResult
            {
                FullName = request.FullName,
                Date = request.Date,
                Email = request.Email
            };
        }
    }
}
