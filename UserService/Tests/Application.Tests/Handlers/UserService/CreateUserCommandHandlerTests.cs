using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Application.Commands.UserService;
using Application.Handlers.UserService;
using Core.Entities;
using Core.Repositories;

namespace Application.Tests.Handlers.UserService
{
    public class CreateUserCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ILogger<CreateUserCommandHandler>> _logger;

        public CreateUserCommandHandlerTests()
        {
            _userRepository = new();
            _mapper = new();
            _logger = new();
        }

        [Fact]
        public async Task Handle_ReturnsId()
        {
            // Arrange
            var request = new CreateUserCommand(); // Create a request object as needed

            _mapper
                .Setup(m => m.Map<User>(request))
                .Returns(new User()); 

            _userRepository
                .Setup(r => r.AddAsync(It.IsAny<User>()))
                .ReturnsAsync(new User { Id = 123 }); 

            var loggerMock = new Mock<ILogger<CreateUserCommandHandler>>();
            var handler = new CreateUserCommandHandler(_userRepository.Object, _mapper.Object, loggerMock.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(123, result); 
        }
    }
}
