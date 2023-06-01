using App.Application.Interfaces;
using App.Domain.Data.Responses;
using App.Infrastructure.Dummyjson.Client;
using App.Infrastructure.Dummyjson.Models.Responses;

namespace App.Infrastructure.Services;

internal class DataService : IDataService
{
    private readonly IDummyjsonClient _dummyjsonClient;

    public DataService(IDummyjsonClient dummyjsonClient)
    {
        _dummyjsonClient = dummyjsonClient ?? throw new ArgumentNullException(nameof(dummyjsonClient));
    }

    public async Task<IEnumerable<Posts>> GetAllPostsTagsAndReactionsAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<PostData> result = await _dummyjsonClient.GetAllPostsTagsAndReactionsAsync(cancellationToken);

        return result.Select(x => new Posts()
        {
            Id = x.Id,
            UserId = x.UserId,
            Tags = (string[])x.Tags.Clone(),
            Reactions = x.Reactions,
        });
    }

    public async Task<IEnumerable<int>> GetAllUserIDFromPostsAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<PostData> result = await _dummyjsonClient.GetAllUserIdFromPostsAsync(cancellationToken);

        return result.Select(x => x.UserId);
    }

    public async Task<IEnumerable<Posts>> GetAllPostsAsync(int userId, CancellationToken cancellationToken = default)
    {
        IReadOnlyList<PostData> result = await _dummyjsonClient.GetAllPostsAsync(userId, cancellationToken);

        return result.Select(x => new Posts()
        {
            Id = x.Id,
            Title = x.Title,
            Body = x.Body,
            UserId = x.UserId,
            Tags = (string[])x.Tags.Clone(),
            Reactions = x.Reactions,
        });
    }

    public async Task<IEnumerable<Todos>> GetAllTodosAsync(int userId, CancellationToken cancellationToken = default)
    {
        IReadOnlyList<TodoData> result = await _dummyjsonClient.GetAllTodosFromTodosAsync(userId, cancellationToken);

        return result.Select(x => new Todos()
        {
            Id = x.Id,
            Todo = x.Todo,
            Completed = x.Completed,
            UserId = x.UserId,
        });
    }

    public async Task<IEnumerable<int>> GetAllUserIDsFromTodosAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<TodoData> result = await _dummyjsonClient.GetAllTodosAsync(cancellationToken);

        return result.Select(x => x.UserId);
    }

    public async Task<IEnumerable<Users>> GetAllUsersWithCardTypeAsync(string cardType, CancellationToken cancellationToken = default)
    {
        IReadOnlyList<UserData> result = await _dummyjsonClient.GetAllUsersIDByCardTypeInformationFromUsersAsync(cardType, cancellationToken);

        return result.Select(x => new Users()
        {
            Id = x.Id,
        });
    }

    public async Task<IEnumerable<Users>> GetUserNameFromUsersAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<UserData> result = await _dummyjsonClient.GetUserNameFromUsersAsync(cancellationToken);

        return result.Select(x => new Users()
        {
            Id = x.Id,
            UserName = x.UserName,
        });
    }
}
