using BackendService.Models.Response.Template.Process;

namespace BackendService.Models.Response.Template.Query
{
    public class QueryResultSingle<T> : ProcessResult where T : class
    {
        public T? Data { get; set; }
        public QueryResultSingle<T> SetResult(bool status, string message, T? data)
        {
            Status = status;
            Message = message;
            Data = data;
            return this;
        }
    }
}
