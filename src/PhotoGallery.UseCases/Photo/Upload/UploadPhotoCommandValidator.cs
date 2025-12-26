using FluentValidation;

namespace PhotoGallery.UseCases.Photo.Upload;

public class UploadPhotoCommandValidator : AbstractValidator<UploadPhotoCommand>
{
    public UploadPhotoCommandValidator()
    {
        RuleFor(command => command.AlbumId)
            .NotEmpty();

        RuleFor(command => command.UserId)
            .NotEmpty();

        RuleFor(command => command.FileName)
            .NotEmpty();
    }
}
