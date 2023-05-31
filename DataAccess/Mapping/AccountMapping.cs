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
                }
            );
    }
}