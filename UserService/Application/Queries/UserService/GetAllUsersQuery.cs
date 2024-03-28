using MediatR;
using Application.Responses;

namespace Application.Queries.UserService
{
    public class GetAllUsersQuery : IRequest<List<UserResponse>>
    {

    }
}
