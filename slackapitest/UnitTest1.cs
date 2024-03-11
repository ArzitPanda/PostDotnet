using Microsoft.AspNetCore.Mvc;
using Moq;
using SlackApi.Controllers;
using SlackApi.Data.Model;
using SlackApi.Data.Repository;
using SlackApi.Services.AuthService;
using SlackApi.Services.UserService;

namespace slackapitest
{
    public class UnitTest1
    {
        [Fact]
        public async Task GetUserById_ReturnsOkObjectResult_WithValidId()
        {
            // Arrange
            long userId = 1;
            var mockUserService = new Mock<IUserService>();
            var expectedUser = new User(); // Replace User with your actual user model
            mockUserService.Setup(service => service.GetUserById(userId))
                           .ReturnsAsync(expectedUser);
            var controller = new UserController(mockUserService.Object);

            // Act
            var result = await controller.GetUserById(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualUser = Assert.IsAssignableFrom<User>(okResult.Value);
            Assert.Equal(expectedUser, actualUser);
        }

        [Fact]
        public async Task GetUserById_ReturnsNotFound_WithInvalidId()
        {
            // Arrange
            long invalidUserId = -1;
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.GetUserById(invalidUserId))
                           .ReturnsAsync((User)null); // Assuming GetUserById returns null for invalid id
            var controller = new UserController(mockUserService.Object);

            // Act
            var result = await controller.GetUserById(invalidUserId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetUserById_ReturnsInternalServerError_OnServiceError()
        {
            // Arrange
            long userId = 1;
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.GetUserById(userId))
                           .ThrowsAsync(new Exception("Service error")); // Simulating a service error
            var controller = new UserController(mockUserService.Object);

            // Act
            var result = await controller.GetUserById(userId);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode); // Assuming internal server error is returned
        }

    }
}