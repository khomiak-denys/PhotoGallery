using MediatR;

namespace PhotoGallery.UseCases.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse> { }