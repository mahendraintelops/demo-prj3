using MediatR;
using Application.Responses;

namespace Application.Queries.UserService
{
    public class GetUserByIdQuery : IRequest<UserResponse>
    {
        public int id { get; set; }

        public GetUserByIdQuery(int _id)
        {
            id = _id;
        }
    }
}
