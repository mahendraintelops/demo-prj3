using MediatR;

namespace Application.Commands.UserService
{
    public class DeleteUserCommand : IRequest
    {
        public int Id { get; set; }
    }
}
