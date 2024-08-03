using BackendService.Helper.Result;

namespace BackendService.Helper.Interface
{
    public interface IFileHelper
    {
        public FileOperationResult SaveFile(string path, IFormFile file);
        public FileOperationResult DeleteFile(string path, string filename);
    }
}
