using HotelBooking.Core;
using HotelBooking.UnitTests.Fakes;
using System;
using Xunit;

namespace HotelBooking.UnitTests
{
    public class BookingManagerTest_InlineData
    {
        [Theory]
        [InlineData("02/10/2021", "03/10/2021")]
        public void FindAvailableRoom_StartDateNotToday_ThrowsArgumentException(string startDayS, string endDayS)
        {
            // Arrange
            DateTime startDay = DateTime.Parse(startDayS);
            DateTime endDay = DateTime.Parse(endDayS);
            BookingManager bm = new BookingManager(new FakeBookingRepository(startDay,endDay),new FakeRoomRepository());

            // Act
            Action act = () => bm.FindAvailableRoom(startDay, endDay);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Theory]
        [InlineData("05/10/2021", "03/10/2021")]
        [InlineData("10/10/2021", "03/10/2021")]
        [InlineData("05/11/2021", "03/11/2021")]
        [InlineData("30/10/2021", "20/10/2021")]
        public void FindAvailableRoom_EndDateNotOlderThanStartDate_ThrowsArgumentException(string startDayS, string endDayS)
        {
            // Arrange
            DateTime startDay = DateTime.Parse(startDayS);
            DateTime endDay = DateTime.Parse(endDayS);
            BookingManager bm = new BookingManager(new FakeBookingRepository(startDay, endDay), new FakeRoomRepository());

            // Act
            Action act = () => bm.FindAvailableRoom(startDay, endDay);

            // Assert
        }
    }
}
