using App.Domain.Data.Requests;

namespace App.Application.Interfaces;

public interface IRepositoryService
{
    Task AddOrCreatePostsToDatabaseAsync(IEnumerable<PostsToSave> data, CancellationToken cancellationToken = default);
    
    Task AddOrCreateUsersActivityCountToDatabaseAsync(IEnumerable<UsersActivityCount> data, CancellationToken cancellationToken = default);
}
