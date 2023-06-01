using App.Domain.Data.Responses;

namespace App.Application.Interfaces;

public interface IDataService
{
    Task<IEnumerable<Posts>> GetAllPostsAsync(CancellationToken cancellationToken = default);
    
    Task<IEnumerable<Posts>> GetAllPostsAsync(int userId, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<Posts>> GetAllPostsTagsAndReactionsAsync(CancellationToken cancellationToken = default);
        
    Task<IEnumerable<Todos>> GetAllTodosAsync(int userId, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<int>> GetAllUserIDsFromTodosAsync(CancellationToken cancellationToken = default);
    
    Task<IEnumerable<Users>> GetAllUsersWithCardTypeAsync(string cardType, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<Users>> GetUserNameFromUsersAsync(CancellationToken cancellationToken = default);
}
