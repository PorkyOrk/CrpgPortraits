namespace CrpgP.Application.Exceptions;

[Serializable]
public class EntityDeserializationException : Exception
{
    public EntityDeserializationException()
    {
    }
    
    public EntityDeserializationException(string message) : base(message)
    {
    }
    
    public EntityDeserializationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}