using AutoMapper;
using Moq;
using Application.Handlers.UserService;
using Application.Queries.UserService;
using Application.Responses;
using Core.Entities;
using Core.Repositories;

namespace Application.Tests.Handlers.UserService
{
    public class GetAllUsersQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsListOfUserResponses()
        {
            // Arrange
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserResponse>();
            });

            var mapper = new Mapper(mapperConfig);

            var obj = new List<User> 
        {
            new User { Id = 1 },
            new User { Id = 2 }

        };

            var RepositoryMock = new Mock<IUserRepository>();
            RepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(obj);

            var query = new GetAllUsersQuery();
            var handler = new GetAllUsersQueryHandler(RepositoryMock.Object, mapper);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<UserResponse>>(result);
            Assert.Equal(obj.Count, result.Count);
           
        }
    }
}
