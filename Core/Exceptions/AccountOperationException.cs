using System;

namespace Core.Exceptions;

public class AccountOperationException : Exception
{
    public AccountOperationException(string message)
        : base(message) {}
}