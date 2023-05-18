namespace Accounts.Application.Permissions;

public class PermissionDto
{
    public PermissionDto(
        long id,
        string name)
    {
        Id = id;
        Name = name;
    }

    public long Id { get; }

    public string Name { get; }
}