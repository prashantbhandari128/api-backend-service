using BackendService.Models.Response.Template.Process;

namespace BackendService.Models.Response.Template.Operation
{
    public class OperationResultSingle<T> : ProcessResult where T : class
    {
        public int RowAffected { get; set; }
        public T? Data { get; set; }
        public OperationResultSingle<T> SetResult(bool status, string message, int rowAffected, T? data)
        {
            Status = status;
            Message = message;
            RowAffected = rowAffected;
            Data = data;
            return this;
        }
    }
}
