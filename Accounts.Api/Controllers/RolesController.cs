using Accounts.Application.Roles;
using Accounts.Application.Roles.Commands;
using Accounts.Application.Roles.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Api.Controllers
{
    [Route("api/roles")]
    public class RolesController : Controller
    {
        private readonly GetRoleListQueryHandler _getRoleListQueryHandler;
        private readonly GetRoleByIdQueryHandler _getRoleByIdQueryHandler;
        private readonly DeleteRoleCommandHandler _deleteRoleCommandHandler;
        private readonly AddPrivilegeCommandHandler _addPrivilegeCommandhandler;

        public RolesController(
            GetRoleListQueryHandler getRoleListQueryHandler,
            GetRoleByIdQueryHandler getRoleByIdQueryHandler,
            DeleteRoleCommandHandler deleteRoleCommandHandler,
            AddPrivilegeCommandHandler addPrivilegeCommandHandler)
        {
            _getRoleListQueryHandler = getRoleListQueryHandler;
            _getRoleByIdQueryHandler = getRoleByIdQueryHandler;
            _deleteRoleCommandHandler = deleteRoleCommandHandler;
            _addPrivilegeCommandhandler = addPrivilegeCommandHandler;
        }

        [HttpGet("all")]
        public Task<IEnumerable<RoleDto>> FindById([FromQuery] GetRoleListQuery getRoleListQuery)
        {
            return _getRoleListQueryHandler.Handle(getRoleListQuery);
        }

        [HttpGet("find/{id}")]
        public Task<RoleDto> FindById([FromRoute] GetRoleByIdQuery getRoleByIdQuery)
        {
            return _getRoleByIdQueryHandler.Handle(getRoleByIdQuery);
        }

        [HttpGet("delete")]
        public Task FindById([FromQuery] DeleteRoleCommand deleteRoleCommand)
        {
            return _deleteRoleCommandHandler.Handle(deleteRoleCommand);
        }

        [HttpPost("add-privilege")]
        public Task AddPrivilege([FromBody] AddPrivilegeCommand addPrivilegeCommand)
        {
            return _addPrivilegeCommandhandler.Handle(addPrivilegeCommand);
        }
    }
}
