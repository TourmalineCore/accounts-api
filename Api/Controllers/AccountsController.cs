using Application.Accounts;
using Application.Accounts.Commands;
using Application.Accounts.Queries;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Filters;

namespace Api.Controllers;

[Authorize]
[Route("api/accounts")]
public class AccountsController : Controller
{
    private readonly GetAccountsQueryHandler _getAccountsQueryHandler;
    private readonly GetAccountByIdQueryHandler _getAccountByIdQueryHandler;
    private readonly AccountCreationCommandHandler _accountCreationCommandHandler;
    private readonly AccountUpdateCommandHandler _accountUpdateCommandHandler;
    private readonly AccountBlockCommandHandler _accountBlockCommandHandler;
    private readonly AccountUnblockCommandHandler _accountUnblockCommandHandler;

    public AccountsController(
        GetAccountsQueryHandler getAccountsQueryHandler,
        AccountCreationCommandHandler accountCreationCommandHandler,
        AccountUpdateCommandHandler accountUpdateCommandHandler,
        GetAccountByIdQueryHandler getAccountByIdQueryHandler,
        AccountBlockCommandHandler accountBlockCommandHandler,
        AccountUnblockCommandHandler accountUnblockCommandHandler)
    {
        _getAccountsQueryHandler = getAccountsQueryHandler;
        _accountCreationCommandHandler = accountCreationCommandHandler;
        _accountUpdateCommandHandler = accountUpdateCommandHandler;
        _getAccountByIdQueryHandler = getAccountByIdQueryHandler;
        _accountBlockCommandHandler = accountBlockCommandHandler;
        _accountUnblockCommandHandler = accountUnblockCommandHandler;
    }

    [RequiresPermission(Permissions.ViewAccounts)]
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<AccountDto>>> GetAllAsync()
    {
        try
        {
            var accounts = await _getAccountsQueryHandler.HandleAsync(new GetAccountsQuery
                    {
                        CallerCorporateEmail = User.GetCorporateEmail(),
                    }
                );

            return Ok(accounts);
        }
        catch (Exception ex)
        {
            return GetProblem(ex);
        }
    }

    [RequiresPermission(Permissions.ViewAccounts)]
    [HttpGet("findById/{accountId:long}")]
    public async Task<ActionResult<AccountDto>> GetByIdAsync(long accountId)
    {
        try
        {
            var account = await _getAccountByIdQueryHandler.HandleAsync(new GetAccountByIdQuery
                    {
                        Id = accountId,
                        CallerCorporateEmail = User.GetCorporateEmail(),
                    }
                );

            return Ok(account);
        }
        catch (Exception ex)
        {
            return GetProblem(ex);
        }
    }

    [RequiresPermission(Permissions.ManageAccounts)]
    [HttpPost("create")]
    public Task<long> CreateAsync([FromBody] AccountCreationCommand accountCreationCommand)
    {
        var jwtToken = GetJwtTokenAsync(HttpContext);
        accountCreationCommand.AccessToken = jwtToken;
        return _accountCreationCommandHandler.HandleAsync(jwtToken, accountCreationCommand);
    }

    [RequiresPermission(Permissions.ManageAccounts)]
    [HttpPost("edit")]
    public async Task<ActionResult> EditAsync([FromBody] AccountUpdateCommand accountUpdateCommand)
    {
        try
        {
            accountUpdateCommand.CallerCorporateEmail = User.GetCorporateEmail();
            await _accountUpdateCommandHandler.HandleAsync(accountUpdateCommand);
            return Ok();
        }
        catch (Exception ex)
        {
            return GetProblem(ex);
        }
    }

    [RequiresPermission(Permissions.ManageAccounts)]
    [HttpPost("{accountId:long}/block")]
    public async Task<ActionResult> BlockAsync(long accountId)
    {
        try
        {
            await _accountBlockCommandHandler.HandleAsync(new AccountBlockCommand
                    {
                        Id = accountId,
                        CallerCorporateEmail = User.GetCorporateEmail(),
                    }
                );

            return Ok();
        }
        catch (Exception ex)
        {
            return GetProblem(ex);
        }
    }

    [RequiresPermission(Permissions.ManageAccounts)]
    [HttpPost("{accountId:long}/unblock")]
    public async Task<ActionResult> UnblockAsync(long accountId)
    {
        try
        {
            await _accountUnblockCommandHandler.HandleAsync(new AccountUnblockCommand
                    {
                        Id = accountId,
                        CallerCorporateEmail = User.GetCorporateEmail(),
                    }
                );

            return Ok();
        }
        catch (Exception ex)
        {
            return GetProblem(ex);
        }
    }

    private ObjectResult GetProblem(Exception exception)
    {
        return Problem(exception.Message, nameof(AccountsController), StatusCodes.Status500InternalServerError);
    }

    private string GetJwtTokenAsync(HttpContext context)
    {
        var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();

        if(authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
        {
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();

            return token;
        }

        throw new Exception("Токен JWT не найден в заголовке Authorization.");
    }
}