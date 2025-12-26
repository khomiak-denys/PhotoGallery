using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using Microsoft.Extensions.Options;
using PhotoGallery.Infrastructure.Options;
using PhotoGallery.UseCases.Abstractions.Services;

namespace PhotoGallery.Infrastructure.Services;

public class S3ObjectStorageService : IObjectStorageService
{
    private readonly ObjectStorageSettings _settings;
    private readonly IAmazonS3 _s3Client;
    private readonly IAmazonS3 _presignClient;
    private readonly string _bucket;
    private readonly Uri? _presignEndpointUri;

    public S3ObjectStorageService(
        IOptions<ObjectStorageSettings> options)
    {
        _settings = options.Value;

        var endpoint = FirstNonEmpty(_settings.Endpoint, Environment.GetEnvironmentVariable("S3_ENDPOINT"));
        var publicEndpoint = FirstNonEmpty(_settings.PublicEndpoint, Environment.GetEnvironmentVariable("S3_PUBLIC_ENDPOINT"));
        var accessKey = FirstNonEmpty(
            _settings.AccessKey,
            Environment.GetEnvironmentVariable("S3_ACCESS_KEY"),
            Environment.GetEnvironmentVariable("OBJECT_STORAGE_USER"));
        var secretKey = FirstNonEmpty(
            _settings.SecretKey,
            Environment.GetEnvironmentVariable("S3_SECRET_KEY"),
            Environment.GetEnvironmentVariable("OBJECT_STORAGE_PASSWORD"));
        _bucket = FirstNonEmpty(
            _settings.Bucket,
            Environment.GetEnvironmentVariable("S3_BUCKET"),
            Environment.GetEnvironmentVariable("OBJECT_STORAGE_BUCKET"));
        var region = FirstNonEmpty(_settings.Region, Environment.GetEnvironmentVariable("S3_REGION")) ?? "us-east-1";

        var credentials = new BasicAWSCredentials(accessKey, secretKey);
        _s3Client = new AmazonS3Client(credentials, BuildConfig(endpoint, region));

        var presignEndpoint = string.IsNullOrWhiteSpace(publicEndpoint)
            ? endpoint
            : publicEndpoint;
        _presignEndpointUri = TryCreateUri(presignEndpoint);
        _presignClient = presignEndpoint == endpoint
            ? _s3Client
            : new AmazonS3Client(credentials, BuildConfig(presignEndpoint, region));

    }

    public Task<string> GetFileUrl(string objectKey, TimeSpan? expires = null)
    {
        if (string.IsNullOrWhiteSpace(objectKey))
        {
            return Task.FromResult(string.Empty);
        }

        var request = new GetPreSignedUrlRequest
        {
            BucketName = _bucket,
            Key = objectKey,
            Expires = DateTime.UtcNow.Add(expires ?? TimeSpan.FromMinutes(10))
        };

        var url = NormalizePresignedUrl(_presignClient.GetPreSignedURL(request));
        return Task.FromResult(url);
    }

    public Task<string> GetUploadUrl(string objectKey, string? contentType = null, TimeSpan? expires = null)
    {
        if (string.IsNullOrWhiteSpace(objectKey))
        {
            return Task.FromResult(string.Empty);
        }

        var request = new GetPreSignedUrlRequest
        {
            BucketName = _bucket,
            Key = objectKey,
            Verb = HttpVerb.PUT,
            Expires = DateTime.UtcNow.Add(expires ?? TimeSpan.FromMinutes(10))
        };

        if (!string.IsNullOrWhiteSpace(contentType))
        {
            request.ContentType = contentType;
        }

        var url = NormalizePresignedUrl(_presignClient.GetPreSignedURL(request));
        return Task.FromResult(url);
    }

    private AmazonS3Config BuildConfig(string? serviceUrl, string region)
    {
        var config = new AmazonS3Config
        {
            ForcePathStyle = true
        };

        if (!string.IsNullOrWhiteSpace(serviceUrl))
        {
            config.ServiceURL = serviceUrl;
            config.UseHttp = serviceUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase);
            config.AuthenticationRegion = region;
            return config;
        }

        config.RegionEndpoint = RegionEndpoint.GetBySystemName(region);
        return config;
    }

    private static string? FirstNonEmpty(params string?[] values)
    {
        foreach (var value in values)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }
        }

        return null;
    }

    private static Uri? TryCreateUri(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        return Uri.TryCreate(value, UriKind.Absolute, out var uri) ? uri : null;
    }

    private string NormalizePresignedUrl(string url)
    {
        if (_presignEndpointUri is null)
        {
            return url;
        }

        if (!Uri.TryCreate(url, UriKind.Absolute, out var presignedUri))
        {
            return url;
        }

        if (!string.Equals(presignedUri.Host, _presignEndpointUri.Host, StringComparison.OrdinalIgnoreCase))
        {
            return url;
        }

        if (string.Equals(presignedUri.Scheme, _presignEndpointUri.Scheme, StringComparison.OrdinalIgnoreCase))
        {
            return url;
        }

        var builder = new UriBuilder(presignedUri)
        {
            Scheme = _presignEndpointUri.Scheme
        };

        if (!_presignEndpointUri.IsDefaultPort)
        {
            builder.Port = _presignEndpointUri.Port;
        }

        return builder.Uri.ToString();
    }
}
