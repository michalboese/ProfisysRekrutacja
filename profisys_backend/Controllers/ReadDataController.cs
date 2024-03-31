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
            return Ok(documents);
        }
        
        [HttpGet("GetDocumentItemsAll")]
        public async Task<ActionResult<DocumentItems>> GetDocumentItemsAll()
        {
            List<DocumentItems> documents = await _readDataRepository.GetDocumentItemsAll();
            return Ok(documents);
        }

        [HttpGet("GetDocumentItems/{documentId}")]
        public async Task<ActionResult<DocumentItems>> GetDocumentItems(int documentId)
        {
            try
            {
                List<DocumentItems> documentItems = await _readDataRepository.GetDocumentItems(documentId);
                return Ok(documentItems);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            
        }

        [HttpGet("GetDocumentsByName/{name}")]
        public async Task<ActionResult<Documents>> GetDocumentsByName(string name)
        {
            List<Documents> documents = await _readDataRepository.GetDocumentsByName(name);
            return Ok(documents);
        }
    }
}
