using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Commands.UserService;
using Application.Queries.UserService;
using Application.Responses;
using System.Net;


namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserServiceController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserServiceController> _logger;
        public UserServiceController(IMediator mediator, ILogger<UserServiceController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        
        [HttpPost(Name = "CreateUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        

        
        [HttpGet(Name = "GetAllUsers")]
        [ProducesResponseType(typeof(IEnumerable<List<UserResponse>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<UserResponse>>> GetAllUsers(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllUsersQuery(), cancellationToken);
            return Ok(response);
        }
        

        
        [HttpGet("{id}", Name = "GetUserById")]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UserResponse>> GetUserById(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User GET request received for ID {id}", id);
            var response = await _mediator.Send(new GetUserByIdQuery(id), cancellationToken);
            return Ok(response);
        }
        

        
        [HttpPut(Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
        

        
        [HttpDelete("{id}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteUser(int id)
        {
            _logger.LogInformation("User DELETE request received for ID {id}", id);
            var cmd = new DeleteUserCommand() { Id = id };
            await _mediator.Send(cmd);
            return NoContent();
        }
        
    }
}
