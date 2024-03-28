using AutoMapper;
using MediatR;
using Application.Queries.UserService;
using Application.Responses;
using Core.Repositories;

namespace Application.Handlers.UserService
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var generatedUser = await _userRepository.GetByIdAsync(request.id);
            var userEntity = _mapper.Map<UserResponse>(generatedUser);
            return userEntity;
        }
    }
}
