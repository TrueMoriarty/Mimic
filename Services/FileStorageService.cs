using Amazon.S3;
using Amazon.S3.Model;
using DAL.EfClasses;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Services;

public interface IFileStorageService
{
    Task<string> PutFileAsync(Stream stream, string key, FileType fileType, string bucket = null);
    Task<Stream> GetFileStreamAsync(string fileName, string bucket = null);
    Task DeleteFileAsync(string key, string bucket = null);
}

internal class S3FileStorageService : IFileStorageService
{
    private readonly IAmazonS3 _amazonS3;

    private readonly string _defaultBucket;
    private readonly string _s3Url;

    public S3FileStorageService(IAmazonS3 amazonS3, IConfiguration configuration)
    {
        _amazonS3 = amazonS3;
        _defaultBucket = configuration.GetSection("S3:Bucket").Value!;
        _s3Url = configuration.GetSection("S3:Url").Value!;
    }

    public async Task<string> PutFileAsync(Stream stream, string key, FileType fileType, string bucket = null)
    {
        string contentType = fileType switch
        {
            FileType.ImageJpeg => "image/jpeg",
            FileType.ImagePng => "image/png",
            FileType.ImageGif => "image/gif",
            _ => "application/octet-stream"
        };

        string currentBucket = GetBucketOrDefault(bucket);

        PutObjectRequest putObjectRequest = new()
        {
            BucketName = GetBucketOrDefault(bucket),
            Key = key,
            ContentType = contentType,
            InputStream = stream
        };

        var res = await _amazonS3.PutObjectAsync(putObjectRequest);

        string url = res.HttpStatusCode == HttpStatusCode.OK ? $"{_s3Url}{currentBucket}/{key}" : string.Empty;
        return await Task.FromResult(url);
    }

    public async Task DeleteFileAsync(string key, string bucket = null)
    {
        string bucketName = GetBucketOrDefault(bucket);
        await _amazonS3.DeleteObjectAsync(bucketName, key);
    }

    public async Task<Stream> GetFileStreamAsync(string fileName, string bucket = null)
    {
        string bucketName = GetBucketOrDefault(bucket);
        MemoryStream ms = new();

        using var res = await _amazonS3.GetObjectAsync(bucketName, fileName);
        await res.ResponseStream.CopyToAsync(ms);

        ms.Position = 0;
        return ms;
    }

    private string GetBucketOrDefault(string bucket) => !string.IsNullOrWhiteSpace(bucket) ? bucket : _defaultBucket;
}