using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class AccountsDbContext : DbContext
{
  public DbSet<Account> Accounts { get; set; }

  public DbSet<Role> Roles { get; set; }

  public DbSet<AccountRole> AccountRoles { get; set; }

  public DbSet<Tenant> Tenants { get; set; }

  public AccountsDbContext(DbContextOptions<AccountsDbContext> options)
    : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

    base.OnModelCreating(modelBuilder);
  }

  public AccountsDbContext() : base() { }
}
