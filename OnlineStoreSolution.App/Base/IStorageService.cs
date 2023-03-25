using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreSolution.App.Base
{
    public interface IStorageService
    {
        string GetFileUrl(string fileName);
        Task SaveFileAsync (Stream fileStream, string fileName);
        Task DeleteFileAsync (string fileName);
    }
}
