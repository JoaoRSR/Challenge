using System.Text.Json.Serialization;

namespace App.Infrastructure.Dummyjson.Models.Responses;

public class UserData
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
}
