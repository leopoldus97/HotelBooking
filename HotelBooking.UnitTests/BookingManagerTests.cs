using System;
using System.Collections.Generic;
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
        public void GetFullyOccupiedDates_StartDateIsMoreThanEndDate_ThrowArgumentException()
        {
            //Arrange
            DateTime startDate = DateTime.Today.AddDays(5);
            DateTime endDate = DateTime.Today.AddDays(2);
            Action act = () => bookingManager.GetFullyOccupiedDates(startDate, endDate);
            //Act
            var rec = Record.Exception(act);
            //Assert
            Assert.IsType<ArgumentException>(rec);
        }

        [Fact]
        public void GetFullyOccupiedDates_StartDayAdd21EndDateAdd25_ReturnsEmptyList()
        {
            //Arrange
            DateTime startDate = DateTime.Today.AddDays(21);
            DateTime endDate = DateTime.Today.AddDays(25);
            //Act
            List<DateTime> fullyOccupiedDates = bookingManager.GetFullyOccupiedDates(startDate, endDate);
            //Assert
            Assert.Empty(fullyOccupiedDates);
        }

        [Fact]
        public void GetFullyOccupiedDates_StartDayAdd10EndDateAdd20_ReturnsListCount10()
        {
            //Arrange
            DateTime startDate = DateTime.Today.AddDays(10);
            DateTime endDate = DateTime.Today.AddDays(20);
            //Act
            List<DateTime> fullyOccupiedDates = bookingManager.GetFullyOccupiedDates(startDate, endDate);
            //Assert
            Assert.Equal(11, fullyOccupiedDates.Count);
        }
    }
}
