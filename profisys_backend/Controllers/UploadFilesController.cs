using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using profisys_backend.Entities;
using profisys_backend.Model;
using profisys_backend.Repositories.Contracts;
using System.Globalization;

namespace profisys_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFilesController : ControllerBase
    {
        private readonly IUploadFilesRepository _uploadFilesRepository;

        public UploadFilesController(IUploadFilesRepository uploadFilesRepository)
        {
            _uploadFilesRepository = uploadFilesRepository;
        }

        [HttpPost("DeleteData")]
        public IActionResult DeleteData()
        {
            _uploadFilesRepository.DeleteData();

            string[] files = Directory.GetFiles("UploadedFiles/");
            foreach (string file in files)
            {
                System.IO.File.Delete(file);
            }

            return Ok();
        }


        [HttpPost("UploadFile")]
        public async Task<ActionResult<UploadFileResponse>> UploadFile([FromForm] UploadFileRequest request)
        {
            UploadFileResponse response = new UploadFileResponse();
            string path = "UploadedFiles/" + request.File.FileName;

            try
            {
                using (FileStream stream = new FileStream(path, FileMode.CreateNew))
                {
                    await request.File.CopyToAsync(stream);
                }

                response = await _uploadFilesRepository.UploadFileAsync(request, path);

                string[] files = Directory.GetFiles("UploadedFiles/");
                foreach (string file in files)
                {
                    System.IO.File.Delete(file);
                }

            } catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

    }
}
