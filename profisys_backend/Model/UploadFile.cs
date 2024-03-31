namespace profisys_backend.Model
{
    public class UploadFileRequest
    {
        public IFormFile File { get; set; } = null!;
    }

    public class UploadFileResponse
    {
        public bool IsSuccess{ get; set; }
        public string Message { get; set; } = null!;
    }
}
