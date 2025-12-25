using FluentValidation;

namespace PhotoGallery.UseCases.Photo.GetAll;

public class GetPhotosQueryValidator : AbstractValidator<GetPhotosQuery>
{
    public GetPhotosQueryValidator()
    {
        RuleFor(p => p.Page)
            .InclusiveBetween(1, int.MaxValue);

        RuleFor(p => p.PageSize)
            .InclusiveBetween(0, 100);
    }
}
