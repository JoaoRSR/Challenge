using System.Text.Json.Serialization;

namespace App.Infrastructure.Dummyjson.Models.Responses;

public class PostResponse : BaseResponse
{
    [JsonPropertyName("posts")]
    public PostData[] Posts { get; set; }
}
