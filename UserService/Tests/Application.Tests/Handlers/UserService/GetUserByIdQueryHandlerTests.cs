using AutoMapper;
using Moq;
using Application.Handlers.UserService;
using Application.Queries.UserService;
using Application.Responses;
using Core.Entities;
using Core.Repositories;

namespace Application.Tests.Handlers.UserService
{
    public class GetUserByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsUserResponse()
        {
            // Arrange
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserResponse>();
            });

            var mapper = new Mapper(mapperConfig);

            var Id = 1; 
            var obj = new User { Id = Id, /* other properties */ };

            var RepositoryMock = new Mock<IUserRepository>();
            RepositoryMock.Setup(repo => repo.GetByIdAsync(Id)).ReturnsAsync(obj);

            var query = new GetUserByIdQuery(Id);
            var handler = new GetUserByIdQueryHandler(RepositoryMock.Object, mapper);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserResponse>(result);
            // Add assertions to check the mapping and properties 
            Assert.Equal(Id, result.Id);
            // Add more assertions as needed
        }
    }
}
