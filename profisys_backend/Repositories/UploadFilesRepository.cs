

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
        private readonly IReadDataRepository _readDataRepository;

        public UploadFilesRepository(DataContext context, IReadDataRepository readDataRepository)
        {
            _context = context;
            _readDataRepository = readDataRepository;
        }

        public async Task UploadDataFromDirectory()
        {
            var checkDocuments = await _readDataRepository.GetDocumentsAsync();
            var checkDocumentItems = await _readDataRepository.GetDocumentItemsAll();
            
            if (checkDocuments.Count > 0 || checkDocumentItems.Count > 0)
            {
                DeleteData();
            }
            
            var files = Directory.GetFiles("Documents");
            List<Documents> documents = new List<Documents>();
            List<DocumentItems> documentItems = new List<DocumentItems>();

            foreach (var file in files)
            {
                if(!file.EndsWith(".csv"))
                {
                    continue;
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
                
                using (var csv = new CsvReader(new StreamReader(file), config))
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

                        try
                        {
                            _context.Documents.AddRange(documents);
                            await _context.SaveChangesAsync();

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        
                    }
                    else
                    {
                        var recordsItems = csv.GetRecords<DocumentItems>();

                        foreach (var record in recordsItems)
                        {
                            documentItems.Add(record);
                        }

                        try
                        {
                            _context.DocumentItems.AddRange(documentItems);
                            await _context.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
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
            var checkDocuments = await _readDataRepository.GetDocumentsAsync();
            var checkDocumentItems = await _readDataRepository.GetDocumentItemsAll();
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
            
            using (var csv = new CsvReader(new StreamReader(request.File.OpenReadStream()), config))
            {
                csv.Read();
                csv.ReadHeader();

                if (!csv.HeaderRecord.Contains("DocumentId"))
                {
                    if (checkDocuments.Count > 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Documents already uploaded";
                        return response;
                    }
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

                if (checkDocumentItems.Count > 0)
                {
                    response.IsSuccess = false;
                    response.Message = "Items already uploaded";
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
