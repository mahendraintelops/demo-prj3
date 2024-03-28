using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Commands.UserService;
using Core.Entities;
using Core.Repositories;

namespace Application.Handlers.UserService
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, ILogger<CreateUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userEntity = _mapper.Map<User>(request);

            /*****************************************************************************/
            var generatedUser = await _userRepository.AddAsync(userEntity);
            /*****************************************************************************/
            _logger.LogInformation($" {generatedUser} successfully created.");
            return generatedUser.Id;
        }
    }
}
