namespace BackendService.Models.Response.Template.Process
{
    public class ProcessResult
    {
        public bool Status { get; set; }
        public string Message { get; set; } = string.Empty;

        public ProcessResult SetResult(bool status, string message)
        {
            Status = status;
            Message = message;
            return this;
        }
    }

}
