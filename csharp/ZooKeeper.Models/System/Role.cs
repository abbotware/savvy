namespace Savvy.ZooKeeper.Models;

public class Role : SystemEntity
{
}

public class Permission : SystemEntity
{
}

public class RolePermission : SystemEntity
{
    public Role Role { get; set; } = null!;

    public Permission Permission { get; set; } = null!;

    public UserEntity? Target { get; }

    public bool Inheritable { get; set; }

    public bool Deny { get; set; }
}

public class EmployeeRole : SystemEntity
{
    public Employee Employee { get; set; } = null!;

    public Role Role { get; set; } = null!;
}

