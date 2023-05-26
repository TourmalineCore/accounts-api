using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;

namespace DataAccess.Mapping;

internal class AccountMapping : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        var accountsCreatedAtUtc = DateTime.SpecifyKind(new DateTime(2020,
                        01,
                        01,
                        0,
                        0,
                        0
                    ),
                DateTimeKind.Utc
            );

        builder.HasIndex(user => user.CorporateEmail)
            .IsUnique();

        builder.HasData(
                new
                {
                    Id = 1L,
                    CorporateEmail = "ceo@tourmalinecore.com",
                    FirstName = "Ceo",
                    LastName = "Ceo",
                    MiddleName = "Ceo",
                    IsBlocked = false,
                    CreatedAt = Instant.FromDateTimeUtc(accountsCreatedAtUtc),
                }, 
                new
                {
                    Id = 2L,
                    CorporateEmail = "inner-circle-admin@tourmalinecore.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    MiddleName = "Admin",
                    IsBlocked = false,
                    CreatedAt = Instant.FromDateTimeUtc(accountsCreatedAtUtc),
                }
            );
    }
}