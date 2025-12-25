using PhotoGallery.API.Apis.Auth.Responses;
using PhotoGallery.API.Apis.Requests;
using PhotoGallery.API.Common.Abstractions;

namespace PhotoGallery.API.Apis.Auth;

public class Login : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/login", HandleLogin)
            .AllowAnonymous()
            .Produces<LoginResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest);
    }

    private static async Task<IResult> HandleLogin(LoginUserRequest loginRequest, [AsParameters] AuthServices services)
    {
       var result = await  services.Mediator.Send(loginRequest.ToCommand());
       
       var response = services.Mapper.Map<LoginResponse>(result);

       return Results.Ok(response);
    }
}