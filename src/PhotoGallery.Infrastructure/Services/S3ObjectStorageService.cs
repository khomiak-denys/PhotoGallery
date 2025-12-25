using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using PhotoGallery.Infrastructure.Options;
using PhotoGallery.UseCases.Abstractions.Services;

namespace PhotoGallery.Infrastructure.Services;

public class S3ObjectStorageService : IObjectStorageService
{
    private readonly ObjectStorageSettings _settings;
    private readonly IAmazonS3 _s3Client;
    private readonly IAmazonS3 _presignClient;

    public S3ObjectStorageService(IOptions<ObjectStorageSettings> options)
    {
        _settings = options.Value;

        var credentials = new BasicAWSCredentials(_settings.AccessKey, _settings.SecretKey);
        _s3Client = new AmazonS3Client(credentials, BuildConfig(_settings.Endpoint));

        var presignEndpoint = string.IsNullOrWhiteSpace(_settings.PublicEndpoint)
            ? _settings.Endpoint
            : _settings.PublicEndpoint;
        _presignClient = presignEndpoint == _settings.Endpoint
            ? _s3Client
            : new AmazonS3Client(credentials, BuildConfig(presignEndpoint));
    }

    public Task<string> GetFileUrl(string objectKey, TimeSpan? expires = null)
    {
        if (string.IsNullOrWhiteSpace(objectKey))
        {
            return Task.FromResult(string.Empty);
        }

        var request = new GetPreSignedUrlRequest
        {
            BucketName = _settings.Bucket,
            Key = objectKey,
            Expires = DateTime.UtcNow.Add(expires ?? TimeSpan.FromMinutes(10))
        };

        return Task.FromResult(_presignClient.GetPreSignedURL(request));
    }

    private AmazonS3Config BuildConfig(string serviceUrl)
    {
        return new AmazonS3Config
        {
            ServiceURL = serviceUrl,
            ForcePathStyle = true,
            RegionEndpoint = RegionEndpoint.GetBySystemName(_settings.Region)
        };
    }
}
