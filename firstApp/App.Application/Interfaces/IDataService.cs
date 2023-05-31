using App.Domain.Data;

namespace App.Application.Interfaces;

public interface IDataService
{
    Task<IEnumerable<Posts>> GetAllPostsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Todos>> GetAllTodosAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Todos>> GetAllTodosAsync(int userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Users>> GetAllUserIDsFromTodosAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Users>> GetAllUsersWithCardTypeAsync(string cardType, CancellationToken cancellationToken = default);
}
