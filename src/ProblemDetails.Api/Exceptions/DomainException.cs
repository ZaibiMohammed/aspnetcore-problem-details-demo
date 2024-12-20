namespace ProblemDetails.Api.Exceptions;

public class DomainException : Exception
{
    public string Code { get; }

    public DomainException(string message, string code = "domain_error") 
        : base(message)
    {
        Code = code;
    }
}