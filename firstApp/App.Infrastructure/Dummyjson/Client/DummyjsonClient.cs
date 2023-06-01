using App.Infrastructure.Dummyjson.Configurations;
using App.Infrastructure.Dummyjson.Models.Responses;
using Microsoft.Extensions.Options;
using RestSharp;

namespace App.Infrastructure.Dummyjson.Client;

public class DummyjsonClient : IDummyjsonClient
{
    private readonly DummyjsonConfiguration _configuration;
    private readonly RestClient _client;

    public DummyjsonClient(
        IOptions<DummyjsonConfiguration> configuration)
    {
        _configuration = configuration?.Value ?? throw new ArgumentNullException(nameof(configuration));

        var options = new RestClientOptions(_configuration.BaseUrl);
        _client = new RestClient(options, useClientFactory: true);
    }

    public async Task<IReadOnlyList<PostData>> GetAllPostsTagsAndReactionsAsync(CancellationToken cancellationToken = default)
    {
        var request = new RestRequest($"{_configuration.PostDataEndpoint}")
            .AddParameter("select", "userId")
            .AddParameter("select", "tags")
            .AddParameter("select", "reactions")
            .AddParameter("limit", "0");

        var response = await _client.GetAsync<PostResponse>(request, cancellationToken);
        return response.Posts;
    }

    public async Task<IReadOnlyList<PostData>> GetAllPostsAsync(CancellationToken cancellationToken = default)
    {
        var response = await GetAllGenericAsync<PostResponse>(_configuration.PostDataEndpoint, cancellationToken);

        return response.Posts;
    }

    public async Task<IReadOnlyList<PostData>> GetAllPostsAsync(int userId, CancellationToken cancellationToken = default)
    {
        var response = await GetAllGenericAsync<PostResponse>(userId, _configuration.PostDataEndpoint, cancellationToken);

        return response.Posts;
    }

    public async Task<IReadOnlyList<TodoData>> GetAllTodosAsync(CancellationToken cancellationToken = default)
    {
        var response = await GetAllGenericAsync<TodoResponse>(_configuration.TodoDataEndpoint, cancellationToken);

        return response.Todos;
    }

    public async Task<IReadOnlyList<TodoData>> GetAllTodosFromTodosAsync(int userId, CancellationToken cancellationToken = default)
    {
        var response = await GetAllGenericAsync<TodoResponse>(userId, _configuration.TodoDataEndpoint, cancellationToken);

        return response.Todos;
    }

    public async Task<IReadOnlyList<UserData>> GetAllUsersIDByCardTypeInformationFromUsersAsync(string cardType, CancellationToken cancellationToken = default)
    {
        var request = new RestRequest($"{_configuration.UserDataEndpoint}/filter")
            .AddParameter("key", "bank.cardType")
            .AddParameter("value", cardType)
            .AddParameter("select", "id")
            .AddParameter("limit", "0");

        var response = await _client.GetAsync<UserResponse>(request, cancellationToken);

        return response.Users;
    }

    public async Task<IReadOnlyList<UserData>> GetUserNameFromUsersAsync(CancellationToken cancellationToken = default)
    {
        var request = new RestRequest($"{_configuration.UserDataEndpoint}")
            .AddParameter("select", "username")
            .AddParameter("limit", "0");

        var response = await _client.GetAsync<UserResponse>(request, cancellationToken);

        return response.Users;
    }

    private async Task<T> GetAllGenericAsync<T>(string endpoint, CancellationToken cancellationToken = default)
    {
        var request = new RestRequest(endpoint)
            .AddParameter("limit", "0");

        var response = await _client.GetAsync<T>(request, cancellationToken);

        return response;
    }

    private async Task<T> GetAllGenericAsync<T>(int userId, string endpoint, CancellationToken cancellationToken = default)
    {
        var request = new RestRequest($"{endpoint}/user/{userId}")
        .AddParameter("limit", "0");

        var response = await _client.GetAsync<T>(request, cancellationToken);

        return response;
    }
}