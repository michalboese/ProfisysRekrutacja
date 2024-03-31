using Microsoft.EntityFrameworkCore;
using profisys_backend.Data;
using profisys_backend.Entities;
using profisys_backend.Repositories.Contracts;

namespace profisys_backend.Repositories
{
    public class ReadDataRepository : IReadDataRepository
    {
        private readonly DataContext _context;

        public ReadDataRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<DocumentItems>> GetDocumentItemsAll()
        {
            var result = await _context.DocumentItems.ToListAsync();
            return result;
        }

        public async Task<List<DocumentItems>> GetDocumentItems(int documentId)
        {
            if (documentId == 0)
            {
                throw new ArgumentException("DocumentId cannot be 0");
            }

            var result = await _context.DocumentItems.Where(x => x.DocumentId == documentId).ToListAsync();

            if (result.Count == 0)
            {
                throw new ArgumentException("No document items found for the given documentId");
            }

            return result;
            
        }

        public async Task<List<Documents>> GetDocumentsAsync()
        {
            var result = await _context.Documents.ToListAsync();
            return result;

        }

        public async Task<List<Documents>> GetDocumentsByName(string name)
        {
            var result = await _context.Documents.Where(x => x.LastName.Contains(name)).ToListAsync();
            return result;
        }
    }
}
