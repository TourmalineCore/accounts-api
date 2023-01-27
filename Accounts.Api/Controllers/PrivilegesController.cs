using Accounts.Application.Privileges;
using Accounts.Application.Privileges.Commands;
using Accounts.Application.Privileges.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Api.Controllers
{
    [Route("api/privileges")]
    public class PrivilegesController : Controller
    {
        private readonly GetPrivilegeListQueryHandler _getPrivilegeListQueryHandler;
        private readonly GetPrivilegeByIdQueryHandler _getPrivilegeByIdQueryHandler;
        private readonly DeletePrivilegeCommandHandler _deletePrivilegeCommandHandler;
        private readonly GetPrivilegesByAccountIdQueryHandler _getPrivilegesByAccountIdQueryHandler;

        public PrivilegesController(
            GetPrivilegeListQueryHandler getPrivilegeListQueryHandler,
            DeletePrivilegeCommandHandler deletePrivilegeCommandHandler,
            GetPrivilegeByIdQueryHandler getPrivilegeByIdQueryHandler,
            GetPrivilegesByAccountIdQueryHandler getPrivilegesByAccountIdQueryHandler)
        {
            _getPrivilegeListQueryHandler = getPrivilegeListQueryHandler;
            _deletePrivilegeCommandHandler = deletePrivilegeCommandHandler;
            _getPrivilegeByIdQueryHandler = getPrivilegeByIdQueryHandler;
            _getPrivilegesByAccountIdQueryHandler = getPrivilegesByAccountIdQueryHandler;
        }

        [HttpGet("all")]
        public Task<IEnumerable<PrivilegeDto>> FindAll([FromQuery] GetPrivilegeListQuery getPrivilegeListQuery)
        {
            return _getPrivilegeListQueryHandler.Handle(getPrivilegeListQuery);
        }

        [HttpGet("find/{Id}")]
        public Task<PrivilegeDto> FindById([FromRoute] GetPrivilegeByIdQuery getPrivilegeByIdQuery)
        {
            return _getPrivilegeByIdQueryHandler.Handle(getPrivilegeByIdQuery);
        }

        [HttpGet("getByAccountId/{accountId}")]
        public Task<IEnumerable<string>> FindByAccountId([FromRoute] long accountId)
        {
            return _getPrivilegesByAccountIdQueryHandler.Handle(accountId);
        }

        [HttpDelete("delete")]
        public Task Delete([FromQuery] DeletePrivilegeCommand deletePrivilegeCommand)
        {
            return _deletePrivilegeCommandHandler.Handle(deletePrivilegeCommand);
        }
    }
}
