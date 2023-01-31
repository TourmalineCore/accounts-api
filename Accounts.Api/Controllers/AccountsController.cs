using System.Net;
using Accounts.Application.Users;
using Accounts.Application.Users.Commands;
using Accounts.Application.Users.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Api.Controllers
{
    [Route("api/accounts")]
    public class AccountsController : Controller
    {
        private readonly GetAccountsQueryHandler _getAccountsQueryHandler;
        private readonly GetAccountByIdQueryHandler _getAccountByIdQueryHandler;
        private readonly AccountCreationCommandHandler _accountCreationCommandHandler;
        private readonly UpdateUserCommandHandler _updateUserCommandHandler;
        private readonly DeleteUserCommandHandler _deleteUserCommandHandler;
        private readonly AddRoleToUserCommandHandler _addRoleToUserCommandHandler;

        private const int CreatedStatusCode = (int)HttpStatusCode.Created;
        private const int InternalServerErrorCode = (int)HttpStatusCode.InternalServerError;

        public AccountsController(
            GetAccountsQueryHandler getAccountsQueryHandler,
            AccountCreationCommandHandler accountCreationCommandHandler,
            UpdateUserCommandHandler updateUserCommandHandler,
            DeleteUserCommandHandler deleteUserCommandHandler,
            AddRoleToUserCommandHandler addRoleToUserCommandHandler,
            GetAccountByIdQueryHandler getAccountByIdQueryHandler)
        {
            _getAccountsQueryHandler = getAccountsQueryHandler;
            _accountCreationCommandHandler = accountCreationCommandHandler;
            _updateUserCommandHandler = updateUserCommandHandler;
            _deleteUserCommandHandler = deleteUserCommandHandler;
            _addRoleToUserCommandHandler = addRoleToUserCommandHandler;
            _getAccountByIdQueryHandler = getAccountByIdQueryHandler;
        }

        [HttpGet("all")]
        public Task<IEnumerable<AccountDto>> FindAll([FromQuery] GetAccountsQuery getAccountsQuery)
        {
            return _getAccountsQueryHandler.Handle(getAccountsQuery);
        }

        [HttpGet("findById/{id}")]
        public Task<AccountDto> FindByIdAsync([FromRoute] GetAccountByIdQuery getAccountByIdQuery)
        {
            return _getAccountByIdQueryHandler.Handle(getAccountByIdQuery);
        }

        [HttpPost("create")]
        public async Task<ActionResult<long>> CreateAsync([FromBody] AccountCreationCommand accountCreationCommand)
        {
            try
            {
                await _accountCreationCommandHandler.HandleAsync(accountCreationCommand);
                return StatusCode(CreatedStatusCode);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, InternalServerErrorCode);
            }
        }

        //TODO: #861ma1b6p - temporary disabled until we get prototypes
        // [HttpPut("update")]
        // public Task Update([FromBody] UpdateUserCommand updateUserCommand)
        // {
        //     return _updateUserCommandHandler.Handle(updateUserCommand);
        // }
        //
        // [HttpDelete("delete")]
        // public Task Delete([FromBody] DeleteUserCommand deleteUserCommand)
        // {
        //     return _deleteUserCommandHandler.Handle(deleteUserCommand);
        // }
        //
        // [HttpPost("add-role")]
        // public Task AddRole([FromBody] AddRoleToUserCommand addRoleToUserCommand)
        // {
        //     return _addRoleToUserCommandHandler.Handle(addRoleToUserCommand);
        // }
    }
}
