namespace CrpgP.Application.Exceptions;

[Serializable]
public class PayloadEmptyException : Exception
{
    public PayloadEmptyException()
    {
    }
    
    public PayloadEmptyException(string message) : base(message)
    {
    }
    
    public PayloadEmptyException(string message, Exception innerException) : base(message, innerException)
    {
    }
}