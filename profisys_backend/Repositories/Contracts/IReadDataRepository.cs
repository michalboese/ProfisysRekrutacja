using profisys_backend.Entities;

namespace profisys_backend.Repositories.Contracts
{
    public interface IReadDataRepository
    {
        Task<List<Documents>> GetDocumentsAsync();
        Task<List<DocumentItems>> GetDocumentItems(int documentId);
        Task<List<Documents>> GetDocumentsByName(string name);
    }
}
