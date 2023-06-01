using App.Infrastructure.Dummyjson.Models.Responses;

namespace App.Infrastructure.Dummyjson.Client;

public interface IDummyjsonClient
{
    Task<IReadOnlyList<PostData>> GetAllPostsAsync(CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<PostData>> GetAllPostsAsync(int userId, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<PostData>> GetAllPostsTagsAndReactionsAsync(CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<TodoData>> GetAllTodosAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TodoData>> GetAllTodosFromTodosAsync(int userId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<UserData>> GetAllUsersIDByCardTypeInformationFromUsersAsync(string cardType, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<UserData>> GetUserNameFromUsersAsync(CancellationToken cancellationToken = default);
}
