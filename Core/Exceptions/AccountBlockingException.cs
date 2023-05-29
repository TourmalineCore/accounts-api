using System;

namespace Core.Exceptions;

public class AccountBlockingException : Exception
{
    public AccountBlockingException(string message)
        : base(message) {}
}