using RoomBookingApp.Models;
using RoomBookingApp.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookingApp.Tests
{
    public class RoomBookingRequestProcessorTest
    {
        RoomBookingRequestProcessor _processor;

        public RoomBookingRequestProcessorTest()
        {
            //Arrange
            _processor = new RoomBookingRequestProcessor();
        }


        [Fact]
        public void Should_Return_Room_Booking_Response_With_Request_Values()
        {
            //Arrange
            var request = new RoomBookingRequest()
            {
                FullName = "Name Test",
                Email = "test@email.com",
                Date = new DateTime(2025, 6, 17)
            };


            //Act
            RoomBookingResult result = _processor.BookRoom(request);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(request.FullName, result.FullName);
            Assert.Equal(request.Email, result.Email);
            Assert.Equal(request.Date, result.Date);
        }


        [Fact]
        public void Should_Throw_Exception_For_Null_Request()
        {
            var processor = new RoomBookingRequestProcessor();
            var exception = Assert.Throws<ArgumentNullException>(() => _processor.BookRoom(null));
            Assert.Equal(exception.ParamName, "request");
        }
    }
}
