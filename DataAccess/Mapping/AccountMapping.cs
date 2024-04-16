using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;

namespace DataAccess.Mapping;

internal class AccountMapping : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasIndex(user => user.CorporateEmail)
            .IsUnique();

        builder.Property(x => x.FirstName).HasMaxLength(50);
        builder.Property(x => x.LastName).HasMaxLength(50);
        builder.Property(x => x.MiddleName).HasMaxLength(50);
        builder.Property(x => x.TenantId).HasDefaultValue(1L);

        builder.HasData(
                new
                {
                    Id = MappingData.AdminAccountId,
                    CorporateEmail = "inner-circle-admin@tourmalinecore.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    MiddleName = "Admin",
                    IsBlocked = false,
                    CreatedAt = Instant.FromDateTimeUtc(MappingData.AccountsCreatedAtUtc),
                    TenantId = 1L
                },
                new
                {
                    Id = MappingData.CeoAccountId,
                    CorporateEmail = "ceo@tourmalinecore.com",
                    FirstName = "Ceo",
                    LastName = "Ceo",
                    MiddleName = "Ceo",
                    IsBlocked = false,
                    CreatedAt = Instant.FromDateTimeUtc(MappingData.AccountsCreatedAtUtc),
                    TenantId = 1L
                }
            );
    }
}