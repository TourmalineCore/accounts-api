using System.Threading.Tasks;
using Accounts.Application.Contracts;
using Accounts.Core.Contracts;

namespace Accounts.Application.Permissions.Commands;

public class DeletePermissionCommand
{
    public long Id { get; set; }
}

public class DeletePermissionCommandHandler : ICommandHandler<DeletePermissionCommand>
{
    private readonly IPermissionsRepository _permissionsRepository;

    public DeletePermissionCommandHandler(IPermissionsRepository permissionsRepository)
    {
        _permissionsRepository = permissionsRepository;
    }

    public async Task Handle(DeletePermissionCommand request)
    {
        var permission = await _permissionsRepository.FindByIdAsync(request.Id);
        await _permissionsRepository.RemoveAsync(permission);
    }
}