namespace BackendService.Helper.Result
{
    public class FileOperationResult
    {
        public bool Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Filename { get; set; } = string.Empty;
        public string? NewFilename { get; set; } = string.Empty;
        public FileOperationResult SetResult(bool status, string message, string? filename ,string? newfilename = null)
        {
            Status = status;
            Message = message;
            Filename = filename;
            NewFilename = newfilename;
            return this;
        }
    }
}
