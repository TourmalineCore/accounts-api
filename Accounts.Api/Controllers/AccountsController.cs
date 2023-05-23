using System.Net;
using Accounts.Application.Users;
using Accounts.Application.Users.Commands;
using Accounts.Application.Users.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Api.Controllers;

[Route("api/accounts")]
public class AccountsController : Controller
{
    private readonly GetAccountsQueryHandler _getAccountsQueryHandler;
    private readonly GetAccountByIdQueryHandler _getAccountByIdQueryHandler;
    private readonly AccountCreationCommandHandler _accountCreationCommandHandler;
    private readonly GetPermissionsByAccountIdQueryHandler _getPermissionsByAccountIdQueryHandler;

    private readonly AccountUpdateCommandHandler _accountUpdateCommandHandler;
    private readonly DeleteUserCommandHandler _deleteUserCommandHandler;
    private readonly AddRoleToUserCommandHandler _addRoleToUserCommandHandler;

    private const int CreatedStatusCode = (int)HttpStatusCode.Created;
    private const int InternalServerErrorCode = (int)HttpStatusCode.InternalServerError;

    public AccountsController(
        GetAccountsQueryHandler getAccountsQueryHandler,
        AccountCreationCommandHandler accountCreationCommandHandler,
        AccountUpdateCommandHandler accountUpdateCommandHandler,
        DeleteUserCommandHandler deleteUserCommandHandler,
        AddRoleToUserCommandHandler addRoleToUserCommandHandler,
        GetAccountByIdQueryHandler getAccountByIdQueryHandler,
        GetPermissionsByAccountIdQueryHandler getPermissionsByAccountIdQueryHandler)
    {
        _getAccountsQueryHandler = getAccountsQueryHandler;
        _accountCreationCommandHandler = accountCreationCommandHandler;

        _accountUpdateCommandHandler = accountUpdateCommandHandler;
        _deleteUserCommandHandler = deleteUserCommandHandler;
        _addRoleToUserCommandHandler = addRoleToUserCommandHandler;
        _getAccountByIdQueryHandler = getAccountByIdQueryHandler;
        _getPermissionsByAccountIdQueryHandler = getPermissionsByAccountIdQueryHandler;
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

    [HttpGet("{accountId}/permissions")]
    public Task<IEnumerable<string>> GetPermissionsByAccountIdAsync([FromRoute] long accountId)
    {
        return _getPermissionsByAccountIdQueryHandler.Handle(accountId);
    }

    [HttpPost("edit")]
    public async Task<ActionResult> EditAsync([FromBody] AccountUpdateCommand accountUpdateCommand)
    {
        try
        {
            await _accountUpdateCommandHandler.Handle(accountUpdateCommand);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, nameof(AccountsController), InternalServerErrorCode);
        }
    }

    //TODO: #861ma1b6p - temporary disabled until we get prototypes
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