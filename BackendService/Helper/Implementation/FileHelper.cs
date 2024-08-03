using BackendService.Helper.Interface;
using BackendService.Helper.Result;

namespace BackendService.Helper.Implementation
{
    public class FileHelper : IFileHelper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileHelper(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        }

        public FileOperationResult SaveFile(string path, IFormFile file)
        {
            if (file == null || string.IsNullOrEmpty(path))
            {
                return new FileOperationResult().SetResult(false, "Missing file or path : Failed to Save.", null);
            }
            try
            {
                string uniqueName = Guid.NewGuid().ToString() + DateTime.Now.ToString("yyyy-MM-dd HH_mm_ss") + file.FileName;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, Path.Combine(path, uniqueName));
                using (var stream = new FileStream(serverFolder, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return new FileOperationResult().SetResult(true, "File Saved Successfully.", file.FileName, uniqueName);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error saving file: {ex.Message}");
                return new FileOperationResult().SetResult(true, ex.Message, file.FileName);
            }
        }

        public FileOperationResult DeleteFile(string path, string filename)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(filename))
            {
                return new FileOperationResult().SetResult(false, "Missing filename or path : Failed to Delete.", filename);
            }
            try
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, path, filename);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return new FileOperationResult().SetResult(true, "File Deleted Successfully.", filename);
                }
                return new FileOperationResult().SetResult(false, $"File Doesnot Exits in '{filePath}'.", filename);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting file: {ex.Message}");
                return new FileOperationResult().SetResult(false, ex.Message, filename);
            }
        }
    }
}
