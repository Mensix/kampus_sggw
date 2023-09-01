namespace KampusSggwBackend.Domain.Exceptions;

using System;
using System.Runtime.Serialization;

public class ResourceNotFoundException : AppException
{
    public ResourceNotFoundException(string message) 
        : base(message)
    {
    }

    public ResourceNotFoundException(string message, Exception inner) 
        : base(message, inner)
    {
    }

    public ResourceNotFoundException(SerializationInfo info, StreamingContext context) 
        : base(info, context)
    {
    }
}
