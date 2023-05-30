using App.Application.Interfaces;
using App.Domain.Data;
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

    public async Task<IEnumerable<Posts>> GetAllPostsAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<PostData> result = await _dummyjsonClient.GetAllPostsAsync(cancellationToken);

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

    public async Task<IEnumerable<Todos>> GetAllTodosAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<TodoData> result = await _dummyjsonClient.GetAllTodosAsync(cancellationToken);

        return result.Select(x => new Todos()
        {
            Id = x.Id,
            Todo = x.Todo,
            Completed = x.Completed,
            UserId = x.UserId,
        });
    }

    public async Task<IEnumerable<Users>> GetAllUsersWithCardTypeAsync(string cardType, CancellationToken cancellationToken = default)
    {
        IReadOnlyList<UserData> result = await _dummyjsonClient.GetAllUsersIDByCardTypeInformationAsync(cardType, cancellationToken);

        return result.Select(x => new Users()
        {
            Id = x.Id,
        });
    }
}
