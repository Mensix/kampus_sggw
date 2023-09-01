namespace KampusSggwBackend.Domain.Exceptions;

using System;
using System.Runtime.Serialization;

public class AuthException : AppException
{
    public AuthException(string message) 
        : base(message)
    {
    }

    public AuthException(string message, Exception inner) 
        : base(message, inner)
    {
    }

    public AuthException(SerializationInfo info, StreamingContext context) 
        : base(info, context)
    {
    }
}
