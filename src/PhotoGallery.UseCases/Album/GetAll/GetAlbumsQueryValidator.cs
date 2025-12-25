using FluentValidation;

namespace PhotoGallery.UseCases.Album.GetAll;

public class GetAlbumsQueryValidator : AbstractValidator<GetAlbumsQuery>
{
    public GetAlbumsQueryValidator()
    {
        RuleFor(a => a.Page)
            .InclusiveBetween(0, int.MaxValue);

        RuleFor(a => a.PageSize)
            .InclusiveBetween(0, int.MaxValue);
    }
}