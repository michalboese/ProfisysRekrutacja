using Microsoft.AspNetCore.Mvc;
using profisys_backend.Entities;
using profisys_backend.Repositories.Contracts;

namespace profisys_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadDataController : ControllerBase
    {
        private readonly IReadDataRepository _readDataRepository;

        public ReadDataController(IReadDataRepository readDataRepository)
        {
            _readDataRepository = readDataRepository;
        }

        [HttpGet("GetDocuments")]
        public async Task<ActionResult<Documents>> GetDocuments()
        {
            List<Documents> documents = await _readDataRepository.GetDocumentsAsync();
            if(!documents.Any())
            {
                return NotFound("No documents found.");
            }
            return Ok(documents);
        }
        
        [HttpGet("GetDocumentItemsAll")]
        public async Task<ActionResult<DocumentItems>> GetDocumentItemsAll()
        {
            List<DocumentItems> documents = await _readDataRepository.GetDocumentItemsAll();
            if(!documents.Any())
            {
                return NotFound("No document items found.");
            }
            return Ok(documents);
        }

        [HttpGet("GetDocumentItems/{documentId}")]
        public async Task<ActionResult<DocumentItems>> GetDocumentItems(int documentId)
        {
                List<DocumentItems> documentItems = await _readDataRepository.GetDocumentItems(documentId);
                if(!documentItems.Any())
                {
                    return NotFound("No items for this document found.");
                }
                return Ok(documentItems);
        }

        [HttpGet("GetDocumentsByName/{name}")]
        public async Task<ActionResult<Documents>> GetDocumentsByName(string name)
        {
            List<Documents> documents = await _readDataRepository.GetDocumentsByName(name);
            if(!documents.Any())
            {
                return NotFound("No documents for this name found.");
            }
            return Ok(documents);
        }
    }
}
