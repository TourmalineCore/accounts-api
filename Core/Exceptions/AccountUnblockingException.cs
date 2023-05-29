using System;

namespace Core.Exceptions;

public class AccountUnblockingException : Exception
{
    public AccountUnblockingException(string message)
        : base(message) {}
}