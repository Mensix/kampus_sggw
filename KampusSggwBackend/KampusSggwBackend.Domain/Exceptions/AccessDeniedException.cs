namespace KampusSggwBackend.Domain.Exceptions;

using System;
using System.Runtime.Serialization;

public class AccessDeniedException : AppException
{
    public AccessDeniedException(string message) 
        : base(message)
    {
    }

    public AccessDeniedException(string message, Exception inner) 
        : base(message, inner)
    {
    }

    public AccessDeniedException(SerializationInfo info, StreamingContext context) 
        : base(info, context)
    {
    }
}
