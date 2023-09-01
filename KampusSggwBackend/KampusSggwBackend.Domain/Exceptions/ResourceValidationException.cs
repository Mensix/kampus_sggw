namespace KampusSggwBackend.Domain.Exceptions;

using System;
using System.Runtime.Serialization;

public class ResourceValidationException : AppException
{
    public ResourceValidationException(string message) 
        : base(message)
    {
    }

    public ResourceValidationException(string message, Exception inner) 
        : base(message, inner)
    {
    }

    public ResourceValidationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
