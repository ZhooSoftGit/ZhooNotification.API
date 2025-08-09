namespace ZhooNotification.API.Services
{
    public interface ICloudflareUploader
    {
        public Task<string> GetPresignedUploadUrlAsync(string fileName, string contentType);
    }
}
