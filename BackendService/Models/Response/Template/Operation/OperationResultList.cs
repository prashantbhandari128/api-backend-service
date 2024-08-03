using BackendService.Models.Response.Template.Process;

namespace BackendService.Models.Response.Template.Operation
{
    public class OperationResultList<T> : ProcessResult where T : class
    {
        public int RowAffected { get; set; }
        public List<T>? Data { get; set; }
        public OperationResultList<T> SetResult(bool status, string message, int rowAffected, List<T>? data)
        {
            Status = status;
            Message = message;
            RowAffected = rowAffected;
            Data = data;
            return this;
        }
    }
}
