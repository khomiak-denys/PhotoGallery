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

    public S3ObjectStorageService(IOptions<ObjectStorageSettings> options)
    {
        _settings = options.Value;

        var credentials = new BasicAWSCredentials(_settings.AccessKey, _settings.SecretKey);
        var config = new AmazonS3Config
        {
            ServiceURL = _settings.Endpoint,
            ForcePathStyle = true,
            RegionEndpoint = RegionEndpoint.GetBySystemName(_settings.Region)
        };

        _s3Client = new AmazonS3Client(credentials, config);
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

        return Task.FromResult(_s3Client.GetPreSignedURL(request));
    }
}
