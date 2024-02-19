using NUnit.Framework;
using Moq;
using Homies.Controllers;
using Homies.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;
using Homies.Models.Event;
using Homies.Models.Type;

namespace Homies.Tests
{
    [TestFixture]
    public class EventControllerTests
    {
        private Mock<IEventService> _mockEventService;
        private EventController _controller;

        [SetUp]
        public void Setup()
        {
            _mockEventService = new Mock<IEventService>();

            var user = new ClaimsPrincipal(new GenericPrincipal(new GenericIdentity("User"), null));
            _controller = new EventController(_mockEventService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext { User = user }
                }
            };
        }

        [Test]
        public async Task Add_ReturnsViewResult()
        {
            // Arrange

            // Act
            var result = await _controller.Add();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public async Task Join_UserNotJoined_ReturnsRedirectToActionResult()
        {
            // Step 1: Arrange - Set up the initial conditions for the test
            // Define the ID of the event to be used in the test
            int eventId = 1;

            // Set up the mock behavior for the event service:
            // Assume the user is not already joined to the event
            _mockEventService.Setup(s => s.IsUserJoinedEventAsync(eventId, It.IsAny<string>())).ReturnsAsync(false);

            // Set up the mock behavior for joining the event:
            // Assume joining the event is successful
            _mockEventService.Setup(s => s.JoinEventAsync(eventId, It.IsAny<string>())).ReturnsAsync(true);

            // Step 2: Act - Perform the action being tested
            // Call the controller method to join the event
            var result = await _controller.Join(eventId);

            // Step 3: Assert - Verify the outcome of the action
            // Assert that the result returned is of the expected type
            Assert.IsInstanceOf<RedirectToActionResult>(result);

            // Convert the result to RedirectToActionResult for further assertions
            var redirectResult = (RedirectToActionResult)result;

            // Assert that the action name and controller name in the redirect result are as expected
            Assert.That(redirectResult.ActionName, Is.EqualTo("Joined"));
            Assert.That(redirectResult.ControllerName, Is.EqualTo("Event"));
        }


        [Test]
        public async Task Join_UserAlreadyJoined_ReturnsRedirectToActionResult()
        {
            // Step 1: Arrange - Set up the initial conditions for the test
            // Define the ID of the event to be used in the test
            int eventId = 1;

            // Set up the mock behavior for the event service:
            // Assume the user is already joined to the event
            _mockEventService.Setup(s => s.IsUserJoinedEventAsync(eventId, It.IsAny<string>())).ReturnsAsync(true);

            // Step 2: Act - Perform the action being tested
            // Call the controller method to join the event
            var result = await _controller.Join(eventId);

            // Step 3: Assert - Verify the outcome of the action
            // Assert that the result returned is of the expected type
            Assert.IsInstanceOf<RedirectToActionResult>(result);

            // Convert the result to RedirectToActionResult for further assertions
            var redirectResult = (RedirectToActionResult)result;

            // Assert that the action name and controller name in the redirect result are as expected
            Assert.That(redirectResult.ActionName, Is.EqualTo("Joined"));
            Assert.That(redirectResult.ControllerName, Is.EqualTo("Event"));
        }

        [Test]
        public async Task Join_JoinEventFails_ReturnsBadRequestResult()
        {
            // Arrange
            int eventId = 1;
            string currentUserId = "User1";

            // Assume joining the event fails
            _mockEventService.Setup(s => s.JoinEventAsync(eventId, currentUserId)).ReturnsAsync(false);

            // Set the user identity in the controller context to simulate a user
            _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new GenericPrincipal(new GenericIdentity(currentUserId), null));

            // Act
            var result = await _controller.Join(eventId);

            // Assert
            // Ensure that the result returned is a BadRequestResult
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task Leave_ReturnsRedirectToActionResult()
        {
            // Arrange
            int eventId = 1;

            // Mock the LeaveEventAsync method to return true, indicating successful leaving
            _mockEventService.Setup(s => s.LeaveEventAsync(eventId, It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _controller.Leave(eventId);

            // Assert
            // Ensure that the result returned is a RedirectToActionResult
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirectResult = (RedirectToActionResult)result;

            // Assert that the action name and controller name in the redirect result are as expected
            Assert.That(redirectResult.ActionName, Is.EqualTo("All"));
            Assert.That(redirectResult.ControllerName, Is.EqualTo("Event"));
        }

        [Test]
        public async Task Details_EventExists_ReturnsViewResultWithEvent()
        {
            // Arrange
            int eventId = 1;

            // Create a mock event model to be returned by the service
            var eventModel = new EventViewModel { Id = eventId, Name = "Event 1" };

            // Mock the GetEventDetailsAsync method to return the mock event model
            _mockEventService.Setup(s => s.GetEventDetailsAsync(eventId)).ReturnsAsync(eventModel);

            // Act
            var result = await _controller.Details(eventId);

            // Assert
            // Ensure that the result returned is a ViewResult
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = (ViewResult)result;

            // Assert that the model in the view result is of the expected type
            Assert.IsInstanceOf<EventViewModel>(viewResult.Model);
            var model = (EventViewModel)viewResult.Model;

            // Assert that the event ID in the model matches the expected event ID
            Assert.That(model.Id, Is.EqualTo(eventId));
        }

        [Test]
        public async Task Edit_EventExistsAndUserIsOrganizer_ReturnsViewResultWithEvent()
        {
            // Arrange
            int eventId = 1;

            // Create a mock event model to be returned by the service
            var eventModel = new EventFormModel { TypeId = eventId, Name = "Event 1" };

            // Mock the GetEventForEditAsync method to return the mock event model
            _mockEventService.Setup(s => s.GetEventForEditAsync(eventId)).ReturnsAsync(eventModel);

            // Act
            var result = await _controller.Edit(eventId);

            // Assert
            // Ensure that the result returned is a ViewResult
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = (ViewResult)result;

            // Assert that the model in the view result is of the expected type
            Assert.IsInstanceOf<EventFormModel>(viewResult.Model);
            var model = (EventFormModel)viewResult.Model;

            // Assert that the event ID in the model matches the expected event ID
            Assert.That(model.TypeId, Is.EqualTo(eventId));
        }

        [Test]
        public async Task Edit_EventExistsButUserIsNotOrganizer_ReturnsUnauthorizedResult()
        {
            // Arrange
            int eventId = 1;
            string currentUserId = "User1";
            string eventOrganiserId = "Organiser1";

            // Create a mock event model to be returned by the service
            var eventModel = new EventFormModel { TypeId = eventId, Name = "Event 1" };

            // Mock the GetEventForEditAsync method to return the mock event model
            _mockEventService.Setup(s => s.GetEventForEditAsync(eventId)).ReturnsAsync(eventModel);

            // Mock the GetEventOrganizerIdAsync method to return the organizer's ID
            _mockEventService.Setup(s => s.GetEventOrganizerIdAsync(eventId)).ReturnsAsync(eventOrganiserId);

            // Set the user identity in the controller context to simulate a user
            _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new GenericPrincipal(new GenericIdentity(currentUserId), null));

            // Act
            var result = await _controller.Edit(eventId);

            // Assert
            // Ensure that the result returned is an UnauthorizedResult
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }

        [Test]
        public async Task Add_Post_ValidEventModel_ReturnsRedirectToActionResult()
        {
            // Arrange
            var eventModel = new EventFormModel { Name = "Event 1", Description = "Description 1", TypeId = 1 };

            // Assume the event type is valid
            _mockEventService.Setup(s => s.GetAllTypesAsync()).ReturnsAsync(new List<TypeViewModel> { new TypeViewModel { Id = 1, Name = "Type 1" } });

            // Assume adding the event is successful
            _mockEventService.Setup(s => s.AddEventAsync(eventModel, It.IsAny<string>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Add(eventModel);

            // Assert
            // Ensure that the result returned is a RedirectToActionResult
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirectResult = (RedirectToActionResult)result;

            // Assert that the action name and controller name in the redirect result are as expected
            Assert.That(redirectResult.ActionName, Is.EqualTo("All"));
            Assert.That(redirectResult.ControllerName, Is.EqualTo("Event"));
        }

        [Test]
        public async Task Add_Post_InvalidEventModel_ReturnsViewResultWithError()
        {
            // Arrange
            var eventModel = new EventFormModel { Name = "Event 1", Description = "Description 1", TypeId = 1 };

            // Assume the event type is invalid
            _mockEventService.Setup(s => s.GetAllTypesAsync()).ReturnsAsync(new List<TypeViewModel>());

            // Assume adding the event is not successful
            _mockEventService.Setup(s => s.AddEventAsync(eventModel, It.IsAny<string>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Add(eventModel);

            // Assert
            // Ensure that the result returned is a ViewResult
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = (ViewResult)result;

            // Assert that the model in the view result is of the expected type
            Assert.IsInstanceOf<EventFormModel>(viewResult.Model);
            var model = (EventFormModel)viewResult.Model;

            // Assert that there are model errors for the TypeId field
            Assert.IsTrue(viewResult.ViewData.ModelState.ContainsKey(nameof(eventModel.TypeId)));
        }

        [Test]
        public async Task Leave_EventNotExists_ReturnsBadRequestResult()
        {
            // Arrange
            int eventId = 1;

            // Assume leaving the event is not successful
            _mockEventService.Setup(s => s.LeaveEventAsync(eventId, It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await _controller.Leave(eventId);

            // Assert
            // Ensure that the result returned is a BadRequestResult
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task All_ReturnsViewResultWithEvents()
        {
            // Arrange
            var events = new List<EventViewModel>
            {
                new EventViewModel { Id = 1, Name = "Event 1" },
                new EventViewModel { Id = 2, Name = "Event 2" }
            };

            // Mock the GetAllEventsAsync method to return events
            _mockEventService.Setup(s => s.GetAllEventsAsync()).ReturnsAsync(events);

            // Act
            var result = await _controller.All();

            // Assert
            // Ensure that the result returned is a ViewResult
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = (ViewResult)result;

            // Ensure that the model in the view result is of the expected type
            Assert.IsInstanceOf<List<EventViewModel>>(viewResult.Model);
            var model = (List<EventViewModel>)viewResult.Model;

            // Ensure that the events in the model match the expected events
            Assert.That(model.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task Details_EventDoesNotExist_ReturnsBadRequestResult()
        {
            // Arrange
            int eventId = 1;

            // Mock the GetEventDetailsAsync method to return null, indicating event does not exist
            _mockEventService.Setup(s => s.GetEventDetailsAsync(eventId)).ReturnsAsync((EventViewModel)null);

            // Act
            var result = await _controller.Details(eventId);

            // Assert
            // Ensure that the result returned is a BadRequestResult
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task Edit_EventDoesNotExist_ReturnsBadRequestResult()
        {
            // Arrange
            int eventId = 1;

            // Mock the GetEventForEditAsync method to return null, indicating event does not exist
            _mockEventService.Setup(s => s.GetEventForEditAsync(eventId)).ReturnsAsync((EventFormModel)null);

            // Act
            var result = await _controller.Edit(eventId);

            // Assert
            // Ensure that the result returned is a BadRequestResult
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task Edit_UserIsNotOrganizer_ReturnsUnauthorizedResult()
        {
            // Arrange
            int eventId = 1;
            string currentUserId = "User1";
            string eventOrganizerId = "Organizer1";

            // Mock the GetEventForEditAsync method to return an event model
            var eventModel = new EventFormModel { TypeId = eventId, Name = "Event 1" };
            _mockEventService.Setup(s => s.GetEventForEditAsync(eventId)).ReturnsAsync(eventModel);

            // Mock the GetEventOrganizerIdAsync method to return a different organizer ID
            _mockEventService.Setup(s => s.GetEventOrganizerIdAsync(eventId)).ReturnsAsync(eventOrganizerId);

            // Set the user identity in the controller context to simulate a user
            _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new GenericPrincipal(new GenericIdentity(currentUserId), null));

            // Act
            var result = await _controller.Edit(eventId);

            // Assert
            // Ensure that the result returned is an UnauthorizedResult
            Assert.IsInstanceOf<UnauthorizedResult>(result);
        }
    }
}
