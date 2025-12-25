using FluentValidation;

namespace PhotoGallery.UseCases.Album.Create;

public class CreateAlbumCommandValidator : AbstractValidator<CreateAlbumCommand>
{
    public CreateAlbumCommandValidator()
    {
        RuleFor(a => a.Name)
            .NotEmpty();
    }
}
