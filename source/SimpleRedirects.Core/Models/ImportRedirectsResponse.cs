using Newtonsoft.Json;

namespace SimpleRedirects.Core.Models;

public class ImportRedirectsResponse : BaseResponse
{
    [JsonProperty("addedRedirects")]
    public Redirect[] AddedRedirects { get; set; }

    [JsonProperty("updatedRedirects")]
    public Redirect[] UpdatedRedirects { get; set; }

    [JsonProperty("deletedRedirects")]
    public Redirect[] DeletedRedirects { get; set; }
}