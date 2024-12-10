namespace MimicWebApi.ConfigModels;

public class S3Config
{
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public string Url { get; set; }
    public string Region { get; set; }
    public string Bucket { get; set; }
}