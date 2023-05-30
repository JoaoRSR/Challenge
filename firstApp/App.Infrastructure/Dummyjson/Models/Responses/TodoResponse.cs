using System.Text.Json.Serialization;

namespace App.Infrastructure.Dummyjson.Models.Responses;

public class TodoResponse : BaseResponse
{
    [JsonPropertyName("todos")]
    public TodoData[] Todos { get; set; }
}
