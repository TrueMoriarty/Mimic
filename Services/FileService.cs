using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;

namespace Services;

public interface IFileService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileStream"></param>
    /// <param name="fileName"></param>
    /// <param name="contentType"> Нужно что-то другое, так как это привязано к http</param>
    Task PutFileAsync(Stream fileStream, string fileName, string contentType, string bucket = null);

    Task<Stream> GetFileAsync(string fileName, string bucket = null);
}

internal class S3FileService : IFileService
{
    private readonly IAmazonS3 _amazonS3;
    private readonly string _defaultBucket;

    public S3FileService(IAmazonS3 amazonS3, IConfiguration configuration)
    {
        _amazonS3 = amazonS3;
        _defaultBucket = configuration.GetSection("S3:Bucket").Value!;
    }

    public async Task PutFileAsync(Stream fileStream, string fileName, string contentType, string bucket = null)
    {
        PutObjectRequest putObjectRequest = new();

        putObjectRequest.BucketName = GetBucketOrDefault(bucket);
        putObjectRequest.Key = fileName;
        putObjectRequest.ContentType = contentType;
        putObjectRequest.InputStream = fileStream;

        var res = await _amazonS3.PutObjectAsync(putObjectRequest);
    }

    //todo: добавить обработку, если нет файла по такому имени или корзине
    public async Task<Stream> GetFileAsync(string fileName, string bucket = null)
    {
        string bucketName = GetBucketOrDefault(bucket);
        using var res = await _amazonS3.GetObjectAsync(bucketName, fileName);

        MemoryStream ms = new();
        await res.ResponseStream.CopyToAsync(ms);
        ms.Position = 0;
        return ms;
    }

    private string GetBucketOrDefault(string bucket) => !string.IsNullOrWhiteSpace(bucket) ? bucket : _defaultBucket;
}