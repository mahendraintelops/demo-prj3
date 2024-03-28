using MediatR;

namespace Application.Commands.UserService
{
    public class CreateUserCommand : IRequest<int>
    {
        public int Id  { get; set; }
    
        
        public string Name { get; set; }
        
    
    }
}
