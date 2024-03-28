using MediatR;
using Microsoft.Extensions.Logging;
using Application.Commands.UserService;
using Application.Exceptions;
using Core.Entities;
using Core.Repositories;

namespace Application.Handlers.UserService
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<DeleteUserCommandHandler> _logger;

        public DeleteUserCommandHandler(IUserRepository userRepository, ILogger<DeleteUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userToDelete = await _userRepository.GetByIdAsync(request.Id);
            if (userToDelete == null)
            {
                throw new UserNotFoundException(nameof(User), request.Id);
            }

            await _userRepository.DeleteAsync(userToDelete);
            _logger.LogInformation($" Id {request.Id} is deleted successfully.");
        }
    }
}
