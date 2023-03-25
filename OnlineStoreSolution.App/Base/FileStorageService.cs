using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreSolution.App.Base
{
    public class FileStorageService : IStorageService
    {
        private readonly IStorageService _storageService;
        private readonly string _userFolderContent;
        private const string USER_FOLDER_NAME = "user-content";

        public FileStorageService(IWebHostEnvironment webHostEnviroment)
        {
            _userFolderContent = Path.Combine(webHostEnviroment.WebRootPath, USER_FOLDER_NAME);
        }
        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_userFolderContent, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }

        public string GetFileUrl(string fileName)
        {
            return $"/{USER_FOLDER_NAME}/{fileName}";
        }

        public async Task SaveFileAsync(Stream fileStream, string fileName)
        {
            var filePath = Path.Combine(_userFolderContent, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await fileStream.CopyToAsync(output);
        }
    }
}
