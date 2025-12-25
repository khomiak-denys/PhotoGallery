using PhotoGallery.API.Apis.Requests;
using PhotoGallery.API.Common.Abstractions;

namespace PhotoGallery.API.Apis.Auth;

public class Register : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("auth/register", HandleRegister)
            .AllowAnonymous()
            .Produces(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status409Conflict);
    }

    private async static Task<IResult> HandleRegister(RegisterUserRequest request, [AsParameters] AuthServices services)
    {
        var command = request.ToCommand();
        await services.Mediator.Send(command);
        return Results.Created();
    }
}