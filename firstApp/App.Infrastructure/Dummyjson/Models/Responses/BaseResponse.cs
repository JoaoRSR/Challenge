using System.Text.Json.Serialization;

namespace App.Infrastructure.Dummyjson.Models.Responses;

public class BaseResponse
{
    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("skip")]
    public int Skip { get; set; }

    [JsonPropertyName("limit")]
    public int Limit { get; set; }
}
