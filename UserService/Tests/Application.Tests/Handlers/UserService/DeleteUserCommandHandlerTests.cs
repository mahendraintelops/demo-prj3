using Microsoft.Extensions.Logging;
using Moq;
using Application.Commands.UserService;
using Application.Exceptions;
using Application.Handlers.UserService;
using Core.Entities;
using Core.Repositories;

namespace Application.Tests.Handlers.UserService
{
    public class DeleteUserCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ILogger<DeleteUserCommandHandler>> _logger;

        public DeleteUserCommandHandlerTests()
        {
            _userRepository = new();
            _logger = new();
        }

        [Fact]
        public async Task Handle_ThrowsUserNotFoundExceptionWhenUserNotFound()
        {
            // Arrange
            var Id = 123; // Replace with the ID you want to test
            var request = new DeleteUserCommand { Id = Id }; // Create a request object

            _userRepository
                .Setup(r => r.GetByIdAsync(Id))
                .ReturnsAsync((User)null); // Mock the repository to return null

            var handler = new DeleteUserCommandHandler(_userRepository.Object, _logger.Object);

            // Act and Assert
            await Assert.ThrowsAsync<UserNotFoundException>(
                async () => await handler.Handle(request, CancellationToken.None)
            );
        }
    }
}
