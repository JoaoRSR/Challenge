using App.Infrastructure.Dummyjson.Models.Responses;

namespace App.Infrastructure.Dummyjson.Client;

public interface IDummyjsonClient
{
    Task<IReadOnlyList<PostData>> GetAllPostsAsync(CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<TodoData>> GetAllTodosAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TodoData>> GetAllTodosAsync(int userId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<UserData>> GetAllUsersIDByCardTypeInformationAsync(string cardType, CancellationToken cancellationToken = default);
}
