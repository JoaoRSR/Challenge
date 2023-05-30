using System.ComponentModel.DataAnnotations;

namespace App.Infrastructure.Dummyjson.Configurations;

public class DummyjsonConfiguration
{
    public const string SectionName = "Dummyjson";

    [Required]
    public string BaseUrl { get; init; }

    [Required]
    public string UserDataEndpoint { get; init; }

    [Required]
    public string PostDataEndpoint { get; init; }

    [Required]
    public string TodoDataEndpoint { get; init; }
}