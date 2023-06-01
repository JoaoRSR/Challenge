using System.Text.Json.Serialization;

namespace App.Infrastructure.Dummyjson.Models.Responses;

public class PostData
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("body")]
    public string Body { get; set; }

    [JsonPropertyName("userId")]
    public int UserId { get; set; }

    [JsonPropertyName("tags")]
    public string[] Tags { get; set; }

    [JsonPropertyName("reactions")]
    public int Reactions { get; set; }
}
