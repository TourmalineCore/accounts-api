using System.Net;
using Accounts.Application.Users;
using Accounts.Application.Users.Commands;
using Accounts.Application.Users.Queries;
using Accounts.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Filters;

namespace Accounts.Api.Controllers;

[Authorize]
[Route("api/accounts")]
public class AccountsController : Controller
{
    private readonly GetAccountsQueryHandler _getAccountsQueryHandler;
    private readonly GetAccountByIdQueryHandler _getAccountByIdQueryHandler;
    private readonly AccountCreationCommandHandler _accountCreationCommandHandler;

    private readonly AccountBlockCommand _accountBlockCommand;
    private readonly AccountUnblockCommand _accountUnblockCommand;

    private readonly AccountUpdateCommandHandler _accountUpdateCommandHandler;

    private const int CreatedStatusCode = (int)HttpStatusCode.Created;
    private const int InternalServerErrorCode = (int)HttpStatusCode.InternalServerError;

    public AccountsController(
        GetAccountsQueryHandler getAccountsQueryHandler,
        AccountCreationCommandHandler accountCreationCommandHandler,
        AccountUpdateCommandHandler accountUpdateCommandHandler,
        GetAccountByIdQueryHandler getAccountByIdQueryHandler,
        AccountBlockCommand accountBlockCommand,
        AccountUnblockCommand accountUnblockCommand)
    {
        _getAccountsQueryHandler = getAccountsQueryHandler;
        _accountCreationCommandHandler = accountCreationCommandHandler;
        _accountUpdateCommandHandler = accountUpdateCommandHandler;
        _getAccountByIdQueryHandler = getAccountByIdQueryHandler;
        _accountBlockCommand = accountBlockCommand;
        _accountUnblockCommand = accountUnblockCommand;
    }

    [RequiresPermission(Permissions.ViewAccounts)]
    [HttpGet("all")]
    public Task<IEnumerable<AccountDto>> FindAll([FromQuery] GetAccountsQuery getAccountsQuery)
    {
        return _getAccountsQueryHandler.Handle(getAccountsQuery);
    }

    [RequiresPermission(Permissions.ViewAccounts)]
    [HttpGet("findById/{id}")]
    public Task<AccountDto> FindByIdAsync([FromRoute] GetAccountByIdQuery getAccountByIdQuery)
    {
        return _getAccountByIdQueryHandler.Handle(getAccountByIdQuery);
    }

    [RequiresPermission(Permissions.ManageAccounts)]
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

    [RequiresPermission(Permissions.ManageAccounts)]
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

    [RequiresPermission(Permissions.ManageAccounts)]
    [HttpPost("{accountId:long}/block")]
    public async Task<ActionResult> BlockAsync([FromRoute] long accountId)
    {
        try
        {
            await _accountBlockCommand.Handle(accountId);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, nameof(AccountsController), InternalServerErrorCode);
        }
    }

    [RequiresPermission(Permissions.ManageAccounts)]
    [HttpPost("{accountId:long}/unblock")]
    public async Task<ActionResult> UnblockAsync([FromRoute] long accountId)
    {
        try
        {
            await _accountUnblockCommand.Handle(accountId);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, nameof(AccountsController), InternalServerErrorCode);
        }
    }
}