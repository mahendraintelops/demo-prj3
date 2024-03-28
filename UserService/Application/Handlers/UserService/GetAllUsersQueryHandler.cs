using AutoMapper;
using MediatR;
using Application.Queries.UserService;
using Application.Responses;
using Core.Repositories;

namespace Application.Handlers.UserService
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<List<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var generatedUser = await _userRepository.GetAllAsync();
            var userEntity = _mapper.Map<List<UserResponse>>(generatedUser);
            return userEntity;
        }
    }
}
