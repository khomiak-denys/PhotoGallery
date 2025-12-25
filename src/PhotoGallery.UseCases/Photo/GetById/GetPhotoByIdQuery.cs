using PhotoGallery.UseCases.Abstractions.Messaging;
using PhotoGallery.UseCases.Photo.Common;

namespace PhotoGallery.UseCases.Photo.GetById;

public class GetPhotoByIdQuery : IQuery<PhotoResult>
{
    public GetPhotoByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; init; }
}
