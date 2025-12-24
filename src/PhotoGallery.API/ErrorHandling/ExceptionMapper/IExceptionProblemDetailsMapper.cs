using Microsoft.AspNetCore.Mvc;

namespace PhotoGallery.API.ErrorHandling.ExceptionMapper;

public interface IExceptionProblemDetailsMapper
{
    ProblemDetails Map(Exception exception);
}