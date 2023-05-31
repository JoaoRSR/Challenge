using System.Text.Json.Serialization;

namespace App.Infrastructure.Dummyjson.Models.Responses;

public class TodoData : UserData
{
    [JsonPropertyName("todo")]
    public string Todo { get; set; }

    [JsonPropertyName("completed")]
    public bool Completed { get; set; }

    [JsonPropertyName("userId")]
    public int UserId { get; set; }
}
