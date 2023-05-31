using System;

namespace Core.Exceptions;

public class RoleOperationException : Exception
{
    public RoleOperationException(string message)
        : base(message) {}
}