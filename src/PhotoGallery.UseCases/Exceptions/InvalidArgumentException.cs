namespace PhotoGallery.UseCases.Exceptions;

public class InvalidArgumentException : Exception
{
    public InvalidArgumentException(string message) : base(message)
    {
    }
}