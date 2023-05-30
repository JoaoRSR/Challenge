using System.Text.Json.Serialization;

namespace App.Infrastructure.Dummyjson.Models.Responses;

public class UserResponse : BaseResponse
{
    [JsonPropertyName("users")]
    public UserData[] Users { get; set; }
}
