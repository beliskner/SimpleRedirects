using SimpleRedirects.Core.Enums;
using SimpleRedirects.Core.Models;

namespace SimpleRedirects.Core.Services.ImportExport;

public interface IImportExportService
{
    DataRecordCollectionFile ExportDataRecordCollection();
    ImportRedirectsResponse ImportRedirectsFromCollection(bool overwriteMatches);
}