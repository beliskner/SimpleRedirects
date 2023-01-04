using System;
using SimpleRedirects.Core.Enums;

namespace SimpleRedirects.Core.Services.ImportExport;

public class ImportExportFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ImportExportFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IImportExportService GetDataRecordProvider(DataRecordProvider dataRecordProvider)
        => (IImportExportService)_serviceProvider.GetService(typeof(CsvImportExportService));
        // ExcelImportExportService disabled until free alternative for EPPlus is used
        /*=> dataRecordProvider == DataRecordProvider.Csv
            ? (IImportExportService)_serviceProvider.GetService(typeof(CsvImportExportService))
            : (IImportExportService)_serviceProvider.GetService(typeof(ExcelImportExportService));*/
}