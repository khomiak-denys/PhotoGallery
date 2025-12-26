using FluentValidation;

namespace PhotoGallery.UseCases.Photo.GetByAlbum;

public class GetAlbumPhotosQueryValidator : AbstractValidator<GetAlbumPhotosQuery>
{
    public GetAlbumPhotosQueryValidator()
    {
        RuleFor(query => query.AlbumId)
            .NotEmpty();

        RuleFor(query => query.Page)
            .InclusiveBetween(1, int.MaxValue);

        RuleFor(query => query.PageSize)
            .InclusiveBetween(0, 100);
    }
}
