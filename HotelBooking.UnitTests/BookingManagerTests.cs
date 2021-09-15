using System;
using HotelBooking.Core;
using HotelBooking.UnitTests.Fakes;
using Xunit;

namespace HotelBooking.UnitTests
{
    public class BookingManagerTests
    {
        private IBookingManager bookingManager;

        public BookingManagerTests()
        {
            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(20);
            IRepository<Booking> bookingRepository = new FakeBookingRepository(start, end);
            IRepository<Room> roomRepository = new FakeRoomRepository();
            bookingManager = new BookingManager(bookingRepository, roomRepository);
        }

        [Fact]
        public void FindAvailableRoom_StartDateNotInTheFuture_ThrowsArgumentException()
        {
            // Arrange
            DateTime date = DateTime.Today;

            // Act
            Action act = () => bookingManager.FindAvailableRoom(date, date);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void FindAvailableRoom_RoomAvailable_RoomIdNotMinusOne()
        {
            // Arrange
            DateTime date = DateTime.Today.AddDays(1);
            // Act
            int roomId = bookingManager.FindAvailableRoom(date, date);
            // Assert
            Assert.NotEqual(-1, roomId);
        }

        [Fact]
        public void CreateBooking_IncorrectStartDate_ReturnsFalse()
        {
            // Arrange
            Booking booking = new()
            {
                StartDate = DateTime.Today.AddDays(11),
                EndDate = DateTime.Today.AddDays(12)
            };

            // Act
            bool isCreated = bookingManager.CreateBooking(booking);

            // Assert
            Assert.False(isCreated);
        }

        [Fact]
        public void CreateBooking_CorrectStartDate_ReturnsTrue()
        {
            // Arrange
            Booking booking = new()
            {
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(2)
            };

            // Act
            bool isCreated = bookingManager.CreateBooking(booking);

            // Assert
            Assert.True(isCreated);
        }

    }
}
