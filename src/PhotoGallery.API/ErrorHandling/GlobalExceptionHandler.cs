using Microsoft.AspNetCore.Diagnostics;
using PhotoGallery.API.ErrorHandling.ExceptionMapper;

namespace PhotoGallery.API.ErrorHandling;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IProblemDetailsService _problemDetailsService;
    private readonly IExceptionProblemDetailsMapper _mapper;
    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger,
        IProblemDetailsService problemDetailsService,
        IExceptionProblemDetailsMapper mapper)
    {
        _logger = logger;
        _problemDetailsService = problemDetailsService;
        _mapper = mapper;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetailsException = _mapper.Map(exception);

        httpContext.Response.StatusCode = problemDetailsException.Status ?? StatusCodes.Status500InternalServerError;

        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = problemDetailsException
        });
    }
}