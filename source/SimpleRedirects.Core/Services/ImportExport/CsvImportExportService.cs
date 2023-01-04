using System.Globalization;
using System.IO;
using CsvHelper;
using SimpleRedirects.Core.Enums;
using SimpleRedirects.Core.Models;

namespace SimpleRedirects.Core.Services.ImportExport;

public class CsvImportExportService : IImportExportService
{
    private readonly RedirectRepository _redirectRepository;

    public CsvImportExportService(RedirectRepository redirectRepository)
    {
        _redirectRepository = redirectRepository;
    }

    public DataRecordCollectionFile ExportDataRecordCollection()
    {
        var records = _redirectRepository.GetAllRedirects();
        using var memoryStream = new MemoryStream();
        using var streamWriter = new StreamWriter(memoryStream);
        using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
        csvWriter.Context.RegisterClassMap<RedirectMap>();
        csvWriter.WriteHeader<Redirect>();
        csvWriter.NextRecord();
        csvWriter.WriteRecords(records);
        csvWriter.Flush();
        streamWriter.Flush();
        memoryStream.Position = 0;

        return new DataRecordCollectionFile(DataRecordProvider.Csv, memoryStream.ToArray());
    }

    public ImportRedirectsResponse ImportRedirectsFromCollection(bool overwriteMatches)
    {
        using var reader = new StreamReader("path\\to\\file.csv");
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<Redirect>();
        return null;
    }
}