using System;
using Newtonsoft.Json;

namespace SimpleRedirects.Core.Models;

public class ImportRedirectsResponse : BaseResponse
{
    [JsonProperty("addedRedirects")]
    public Redirect[] AddedRedirects { get; set; }

    [JsonProperty("updatedRedirects")]
    public Redirect[] UpdatedRedirects { get; set; }

    public static ImportRedirectsResponse EmptyImportRecordResponse()
        => new ImportRedirectsResponse
        {
            Success = false,
            Message = "No valid redirects could be processed.",
            AddedRedirects = Array.Empty<Redirect>(),
            UpdatedRedirects = Array.Empty<Redirect>()
        };
}