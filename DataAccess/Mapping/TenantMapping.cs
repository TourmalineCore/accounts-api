using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping;

public class TenantMapping : IEntityTypeConfiguration<Tenant>
{
  public void Configure(EntityTypeBuilder<Tenant> builder)
  {
    builder
      .HasMany(e => e.Accounts)
      .WithOne(e => e.Tenant)
      .HasForeignKey(e => e.TenantId)
      .IsRequired();

    builder.HasData(
      new
      {
        Id = 1L,
        Name = "TourmalineCore"
      },
      new
      {
        Id = 2L,
        Name = "Test"
      }
    );
  }
}
