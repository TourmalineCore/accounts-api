using System.Threading.Tasks;
using Application.Contracts;
using Core.Contracts;

namespace Application.Roles.Queries;

public readonly struct GetRoleByIdQuery
{
  public long Id { get; init; }
}

public class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, RoleDto>
{
  private readonly IRolesRepository _rolesRepository;

  public GetRoleByIdQueryHandler(IRolesRepository rolesRepository)
  {
    _rolesRepository = rolesRepository;
  }

  public async Task<RoleDto> HandleAsync(GetRoleByIdQuery query)
  {
    var role = await _rolesRepository.GetByIdAsync(query.Id);
    return new RoleDto(role);
  }
}
