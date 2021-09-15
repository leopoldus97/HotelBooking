using System;
using HotelBooking.Core;
using HotelBooking.UnitTests.Fakes;
using Xunit;

namespace HotelBooking.UnitTests
{
    public class BookingManagerTests
    {
        private IBookingManager bookingManager;

        public BookingManagerTests(){
            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(20);
            IRepository<Booking> bookingRepository = new FakeBookingRepository(start, end);
            IRepository<Room> roomRepository = new FakeRoomRepository();
            bookingManager = new BookingManager(bookingRepository, roomRepository);
        }

        [Fact]
        public void FindAvailableRoom_StartDateNotToday_ThrowsArgumentException()
        {
            // Arrange
            DateTime dateToday = DateTime.Today;
            DateTime dateTomorrow = DateTime.Today.AddDays(1);

            // Act
            Action act = () => bookingManager.FindAvailableRoom(dateToday, dateTomorrow);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void FindAvailableRoom_StartDateBeforeToday_ThrowsArgumentException()
        {
            // Arrange
            DateTime dateYesterday = DateTime.Today.AddDays(-1);
            DateTime dateToday = DateTime.Today;

            // Act
            Action act = () => bookingManager.FindAvailableRoom(dateYesterday, dateToday);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void FindAvailableRoom_EndDateNotOlderThanStartDate_ThrowsArgumentException()
        {
            // Arrange
            DateTime dateStart = DateTime.Today.AddDays(1);
            DateTime dateEnd = DateTime.Today;

            // Act
            Action act = () => bookingManager.FindAvailableRoom(dateStart, dateEnd);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void FindAvailableRoom_RoomAvailable_RoomIdNotMinusOne()
        {
            // Arrange
            DateTime dateStart = DateTime.Today.AddDays(1);
            DateTime dateEnd = DateTime.Today.AddDays(2);

            // Act
            int roomId = bookingManager.FindAvailableRoom(dateStart, dateEnd);

            // Assert
            Assert.NotEqual(-1, roomId);
        }

        [Fact]
        public void FindAvailableRoom_RoomNOTAvailable_MinusOne()
        {
            // Arrange
            DateTime dateStart = DateTime.Today.AddDays(10);
            DateTime dateEnd = DateTime.Today.AddDays(20);

            // Act
            int roomId = bookingManager.FindAvailableRoom(dateStart, dateEnd);

            // Assert
            Assert.Equal(-1, roomId);
        }
    }
}
