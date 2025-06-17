using RoomBookingApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookingApp.DataServices
{
    public interface IRoomBookingService
    {
        void Save(RoomBooking roomBooking);

    }
}
