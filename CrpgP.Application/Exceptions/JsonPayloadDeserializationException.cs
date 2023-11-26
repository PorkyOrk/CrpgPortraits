namespace CrpgP.Application.Exceptions;

[Serializable]
public class JsonPayloadDeserializationException : Exception
{
    public JsonPayloadDeserializationException()
    {
    }
    
    public JsonPayloadDeserializationException(string message) : base(message)
    {
    }
    
    public JsonPayloadDeserializationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}