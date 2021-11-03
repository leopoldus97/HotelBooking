using FluentAssertions;
using HotelBooking.Core;
using Moq;
using System;
using TechTalk.SpecFlow;

namespace HotelBooking.Specs.Steps
{
    [Binding]
    public sealed class CreateBookingStepDefinition
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;
        private readonly Mock<IRepository<Booking>> bookingRepo = new();
        private readonly Mock<IRepository<Room>> roomRepo = new();
        private readonly IBookingManager bookingManager;

        public CreateBookingStepDefinition(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            var date = DateTime.Now.AddDays(4);
            var activeBookings = new Booking[3]
            {
                new Booking { StartDate=date, EndDate=date.AddDays(14), IsActive=true, RoomId=1 },
                new Booking { StartDate=date, EndDate=date.AddDays(14), IsActive=true, RoomId=2 },
                new Booking { StartDate=date, EndDate=date.AddDays(14), IsActive=true, RoomId=3 }
            };
            bookingRepo.Setup(x => x.Add(It.IsAny<Booking>()));
            bookingRepo.Setup(x => x.GetAll()).Returns(activeBookings);
            var rooms = new Room[3]
            {
                new Room { Id=1, Description="A" },
                new Room { Id=2, Description="B" },
                new Room { Id = 3, Description = "C" }
            };
            roomRepo.Setup(x => x.GetAll()).Returns(rooms);
            bookingManager = new BookingManager(bookingRepo.Object, roomRepo.Object);
        }

        [When("the method 'CreateBooking' is called")]
        public void WhenTheMethodIsCalled()
        {
            var _startDate = _scenarioContext.Get<DateTime>("startDate");
            var _endDate = _scenarioContext.Get<DateTime>("endDate");
            var booking = new Booking
            {
                StartDate = _startDate,
                EndDate = _endDate
            };
            var result = bookingManager.CreateBooking(booking);
            _scenarioContext.Add("result", result);
        }

        [Then("the result should return (.*)")]
        public void ThenTheResultShouldBeTrue(bool result)
        {
            bool res = _scenarioContext.Get<bool>("result");
            res.Should().Be(result);
        }
    }
}
