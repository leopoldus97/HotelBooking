using FluentAssertions;
using HotelBooking.Core;
using Moq;
using System;
using TechTalk.SpecFlow;

namespace HotelBooking.Specs.Steps
{
    [Binding]
    public sealed class GetFullyOccupiedDatesStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly Mock<IRepository<Booking>> bookingRepo = new();
        private readonly Mock<IRepository<Room>> roomRepo = new();
        private readonly IBookingManager bookingManager;

        private string _result;

        public GetFullyOccupiedDatesStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            bookingManager = new BookingManager(bookingRepo.Object, roomRepo.Object);
        }

        [Given("the start date is (.*)")]
        public void GivenTheFirstDateIs(string date)
        {
            var _startDate = DateTime.Parse(date);
            _scenarioContext.Add("startDate", _startDate);
        }

        [Given("the end date is (.*)")]
        public void GivenTheSecondDateIs(string date)
        {
            var _endDate = DateTime.Parse(date);
            _scenarioContext.Add("endDate", _endDate);
        }

        [When("the method 'GetFullyOccupiedDate' is called error thrown")]
        public void WhenMethodCalledErrorThrown()
        {
            try
            {
                var _startDate = _scenarioContext.Get<DateTime>("startDate");
                var _endDate = _scenarioContext.Get<DateTime>("endDate");
                bookingManager.GetFullyOccupiedDates(_startDate, _endDate);
            } catch (ArgumentException ex)
            {
                _result = ex.Message;
            }
        }

        [Then("the result should be (.*)")]
        public void ThenTheResultShouldBe(string result)
        {
            _result.Should().Be(result);
        }
    }
}
