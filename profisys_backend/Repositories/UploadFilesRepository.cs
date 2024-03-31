

using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using profisys_backend.Data;
using profisys_backend.Entities;
using profisys_backend.Model;
using profisys_backend.Repositories.Contracts;
using System.Data;
using System.Globalization;

namespace profisys_backend.Repositories
{
    public class UploadFilesRepository : IUploadFilesRepository
    {
        private readonly DataContext _context;

        public UploadFilesRepository(DataContext context)
        {
            _context = context;
        }

        public void DeleteData()
        {
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE Documents");
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE DocumentItems");
            
        }

        public async Task<UploadFileResponse> UploadFileAsync(UploadFileRequest request, string path)
        {
            UploadFileResponse response = new UploadFileResponse();
            List<Documents> documents = new List<Documents>();
            List<DocumentItems> documentItems = new List<DocumentItems>();
            response.IsSuccess = true;
            response.Message = "File uploaded successfully";
            
            if (!request.File.FileName.EndsWith(".csv"))
            {
                response.IsSuccess = false;
                response.Message = "Invalid file format";
                return response;
            }

            CultureInfo culture = new CultureInfo("pl-PL");
            culture.NumberFormat.CurrencyDecimalSeparator = ",";

            var config = new CsvConfiguration(culture)
            {
                Delimiter = ";",
                MissingFieldFound = null,
                HeaderValidated = null,
                BadDataFound = null,
            };

            DataTable value = new DataTable();
            using (var csv = new CsvReader(new StreamReader(request.File.OpenReadStream()), config))
            {
                csv.Read();
                csv.ReadHeader();

                if (!csv.HeaderRecord.Contains("DocumentId"))
                {
                    var recordsDocuments = csv.GetRecords<Documents>();

                    foreach (var record in recordsDocuments)
                    {
                        documents.Add(record);
                    }

                    if (documents.Count == 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "No data found in the file";
                        return response;
                    }

                    try
                    {
                        _context.Documents.AddRange(documents);
                        await _context.SaveChangesAsync();

                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.Message = ex.Message;
                    }
                    
                    response.IsSuccess = true;
                    response.Message = "Documents uploaded successfully";
                    return response;
                }

                var recordsItems = csv.GetRecords<DocumentItems>();

                foreach (var record in recordsItems)
                {
                    documentItems.Add(record);
                }

                if (documentItems.Count == 0)
                {
                    response.IsSuccess = false;
                    response.Message = "No data found in the file";
                    return response;
                }

                try
                {
                    _context.DocumentItems.AddRange(documentItems);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = ex.Message;
                }
                
                response.IsSuccess = true;
                response.Message = "Items uploaded successfully";
                return response;
            }
        }
    }
}
