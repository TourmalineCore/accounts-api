using NodaTime;
using System;

namespace Accounts.Application.Services
{
    public class Clock : IClock
    {
        public Instant GetCurrentInstant()
        {
            return Instant.FromDateTimeUtc(DateTime.UtcNow);
        }
    }
}
