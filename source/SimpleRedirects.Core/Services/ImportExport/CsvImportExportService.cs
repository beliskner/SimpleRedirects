using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using Microsoft.AspNetCore.Http;
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

    public ImportRedirectsResponse ImportRedirectsFromCollection(IFormFile file, bool overwriteMatches)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<Redirect>().ToArray();

        if (!records.Any()) return ImportRedirectsResponse.EmptyImportRecordResponse();
        var addList = new List<Redirect>();
        var updateList = new List<Redirect>();

        foreach (var redirect in records)
        {
            if (overwriteMatches && _redirectRepository.FetchRedirectByOldUrl(redirect.OldUrl) is { } match)
            {
                var updated = _redirectRepository.UpdateRedirect(redirect);
                updateList.Add(updated);
            }
            else
            {
                var added = _redirectRepository.AddRedirect(redirect.IsRegex, redirect.OldUrl, redirect.NewUrl, redirect.RedirectCode, redirect.Notes);
                addList.Add(added);
            }
        }
        return new ImportRedirectsResponse
        {
            Success = true,
            Message = $"Redirect import completed, added {addList.Count} redirects{(updateList.Count > 0 ? $" and updated {updateList.Count} redirects" : string.Empty)}.",
            AddedRedirects = addList.ToArray(),
            UpdatedRedirects = updateList.ToArray()
        };
    }
}