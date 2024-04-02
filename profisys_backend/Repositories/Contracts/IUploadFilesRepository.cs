using profisys_backend.Entities;
using profisys_backend.Model;
using System.Runtime.CompilerServices;

namespace profisys_backend.Repositories.Contracts
{
    public interface IUploadFilesRepository
    {
        Task<UploadFileResponse> UploadFileAsync(UploadFileRequest request, string path);
        Task UploadDataFromDirectory();
        void DeleteData();
    }
}
