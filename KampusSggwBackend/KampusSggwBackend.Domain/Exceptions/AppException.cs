namespace KampusSggwBackend.Domain.Exceptions;

using System;
using System.Runtime.Serialization;

public class AppException : Exception
{
    public AppException(string message) 
        : base(message) 
    {
    }

    public AppException(string message, Exception inner) 
        : base(message, inner) 
    { 
    }

    public AppException(SerializationInfo info, StreamingContext context) 
        : base(info, context) 
    { 
    }
}
