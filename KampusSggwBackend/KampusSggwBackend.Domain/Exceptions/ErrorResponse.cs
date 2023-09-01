namespace KampusSggwBackend.Domain.Exceptions;

public class ErrorResponse
{
    public string Code { get; set; }
    public string Description { get; set; }
    public object AdditionalInformation { get; set; }
}
