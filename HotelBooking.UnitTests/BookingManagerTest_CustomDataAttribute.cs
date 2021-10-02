using HotelBooking.Core;
using HotelBooking.UnitTests.Fakes;
using System;
using Xunit;

namespace HotelBooking.UnitTests
{
    public class BookingManagerTest_CustomDataAttribute
    {

        [Theory]
        [CustomData("test.json")]
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
