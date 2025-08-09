namespace ZhooNotification.API.Helper
{
    using Amazon.S3;
    using Amazon.S3.Model;
    using ZhooNotification.API.Services;

    public class CloudflareUploader : ICloudflareUploader
    {
        private readonly IConfiguration _config;
        private readonly IAmazonS3 _s3Client;

        public CloudflareUploader(IConfiguration config)
        {
            _config = config;

            _s3Client = new AmazonS3Client(
                _config["Cloudflare:AccessKey"],
                _config["Cloudflare:SecretKey"],
                new AmazonS3Config
                {
                    ServiceURL = _config["Cloudflare:Endpoint"], // e.g. https://<accountid>.r2.cloudflarestorage.com
                    ForcePathStyle = true,
                    UseHttp = false // Use HTTPS for security
                });
        }

        /// <summary>
        /// Generates a pre-signed URL to allow direct upload to Cloudflare R2
        /// </summary>
        public Task<string> GetPresignedUploadUrlAsync(string fileName, string contentType)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new Exception("File name not provided.");

            var key = $"resumes/{Guid.NewGuid()}_{fileName}";

            var request = new GetPreSignedUrlRequest
            {
                BucketName = _config["Cloudflare:Bucket"],
                Key = key,
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddMinutes(10), // URL valid for 10 minutes
                ContentType = contentType
            };

            var url = _s3Client.GetPreSignedURL(request);

            return Task.FromResult(url);
        }
    }
}
