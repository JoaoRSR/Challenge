﻿using App.Infrastructure.Dummyjson.Configurations;
using App.Infrastructure.Dummyjson.Models.Responses;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
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

    public async Task<IReadOnlyList<PostData>> GetAllPostsAsync(CancellationToken cancellationToken = default)
    {
        var response = await GetAllGenericAsync<PostResponse>(_configuration.PostDataEndpoint, cancellationToken);

        return response.Posts;
    }

    public async Task<IReadOnlyList<TodoData>> GetAllTodosAsync(CancellationToken cancellationToken = default)
    {
        var response = await GetAllGenericAsync<TodoResponse>(_configuration.TodoDataEndpoint, cancellationToken);

        return response.Todos;
    }

    public async Task<IReadOnlyList<TodoData>> GetAllTodosAsync(int userID, CancellationToken cancellationToken = default)
    {
        var request = new RestRequest($"{_configuration.TodoDataEndpoint}/filter")
            .AddParameter("key", "userId")
            .AddParameter("value", userID)
            .AddParameter("limit", "0");

        var response = await _client.GetAsync<TodoResponse>(request, cancellationToken);

        return response.Todos;
    }

    public async Task<IReadOnlyList<UserData>> GetAllUsersIDByCardTypeInformationAsync(string cardType, CancellationToken cancellationToken = default)
    {
        var request = new RestRequest($"{_configuration.UserDataEndpoint}/filter")
            .AddParameter("key", "bank.cardType")
            .AddParameter("value", cardType)
            .AddParameter("select", "id")
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
}