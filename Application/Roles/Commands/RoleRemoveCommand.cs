using System.Threading.Tasks;
using Application.Contracts;
using Core.Contracts;

namespace Application.Roles.Commands;

public readonly struct RoleRemoveCommand
{
    public long Id { get; init; }
}

public class RoleRemoveCommandHandler : ICommandHandler<RoleRemoveCommand>
{
    private readonly IRolesRepository _rolesRepository;

    public RoleRemoveCommandHandler(IRolesRepository rolesRepository)
    {
        _rolesRepository = rolesRepository;
    }

    public async Task HandleAsync(RoleRemoveCommand command)
    {
        var role = await _rolesRepository.GetByIdAsync(command.Id);
        await _rolesRepository.DeleteAsync(role);
    }
}