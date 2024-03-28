using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Commands.UserService;
using Application.Exceptions;
using Core.Entities;
using Core.Repositories;


namespace Application.Handlers.UserService
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUserCommandHandler> _logger;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, ILogger<UpdateUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = await _userRepository.GetByIdAsync(request.Id);
            if (userToUpdate == null)
            {
                throw new UserNotFoundException(nameof(User), request.Id);
            }

            _mapper.Map(request, userToUpdate, typeof(UpdateUserCommand), typeof(User));
            await _userRepository.UpdateAsync(userToUpdate);
            _logger.LogInformation($"User is successfully updated");
        }
    }
}
