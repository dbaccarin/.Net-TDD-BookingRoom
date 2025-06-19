using Moq;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Enums;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using RoomBookingApp.Domain;

namespace RoomBookingApp.Core.Tests
{
    public class RoomBookingRequestProcessorTest
    {
        private RoomBookingRequestProcessor _processor;
        private Mock<IRoomBookingService> _roomBookingServiceMock;
        private RoomBookingRequest _request;
        private List<Room> _availableRooms;

        public RoomBookingRequestProcessorTest()
        {
            //Arrange
            _request = new RoomBookingRequest()
            {
                FullName = "Name Test",
                Email = "test@email.com",
                Date = new DateTime(2025, 6, 17)
            };

            _availableRooms = new List<Room>() { new Room() { Id = 1 } };

            _roomBookingServiceMock = new Mock<IRoomBookingService>();
            _roomBookingServiceMock.Setup(x => x.GetAvailableRooms(_request.Date)).Returns(_availableRooms);

            _processor = new RoomBookingRequestProcessor(_roomBookingServiceMock.Object);

        }


        [Fact]
        public void Should_Return_Room_Booking_Response_With_Request_Values()
        {
            //Act
            RoomBookingResult result = _processor.BookRoom(_request);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(_request.FullName, result.FullName);
            Assert.Equal(_request.Email, result.Email);
            Assert.Equal(_request.Date, result.Date);
        }


        [Fact]
        public void Should_Throw_Exception_For_Null_Request()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => _processor.BookRoom(null));
            Assert.Equal("request", exception.ParamName);
        }

        [Fact]
        public void Should_Save_Room_BookiNg_Request()
        {

            RoomBooking savedBooking = null;

            _roomBookingServiceMock.Setup(x => x.Save(It.IsAny<RoomBooking>()))
                .Callback<RoomBooking>(booking => { savedBooking = booking; });

            _processor.BookRoom(_request);

            _roomBookingServiceMock.Verify(x => x.Save(It.IsAny<RoomBooking>()), Times.Once);

            Assert.NotNull(savedBooking);
            Assert.Equal(_request.FullName, savedBooking.FullName);
            Assert.Equal(_request.Email, savedBooking.Email);
            Assert.Equal(_request.Date, savedBooking.Date);
            Assert.Equal(_availableRooms.First().Id, savedBooking.RoomId);
        }

        [Fact]
        public void Should_Not_Save_Room_BookiNg_Request_If_None_Available()
        {
            _availableRooms.Clear();

            _processor.BookRoom(_request);

            _roomBookingServiceMock.Verify(x => x.Save(It.IsAny<RoomBooking>()), Times.Never);
        }

        [Theory]
        [InlineData(BookingResultFlag.Failure, false)]
        [InlineData(BookingResultFlag.Succcess, true)]
        public void Should_Return_Result_Flag_In_Result(BookingResultFlag bookingResultFlag, bool isAvailable)
        {
            if (!isAvailable)
                _availableRooms.Clear();

            var result = _processor.BookRoom(_request);

            Assert.Equal(bookingResultFlag, result.Flag);
        }


        [Theory]
        [InlineData(1, true)]
        [InlineData(null, false)]
        public void Should_Return_RoomBookingId_In_Result(int? roombookingId, bool isAvailable)
        {
            if (!isAvailable)
            {
                _availableRooms.Clear();
            }
            else
            {
                _roomBookingServiceMock.Setup(x => x.Save(It.IsAny<RoomBooking>()))
                    .Callback<RoomBooking>(booking => { booking.Id = roombookingId.Value; });
            }

            var result = _processor.BookRoom(_request);
            Assert.Equal(roombookingId, result.RoomBookingId);
        }

    }
}
