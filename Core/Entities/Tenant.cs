using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Tenant
{
  public Tenant(string name)
  {
    Name = name;
  }

  private Tenant()
  {
  }

  [Key] public long Id { get; set; }

  public string Name { get; set; }

  public List<Account> Accounts { get; } = new();
}
