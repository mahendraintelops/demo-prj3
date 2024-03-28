using MediatR;

namespace Application.Commands.UserService
{
    public class UpdateUserCommand : IRequest
    {
        public int Id  { get; set; }
    
        
        public string Name { get; set; }
        
    
    }
}
