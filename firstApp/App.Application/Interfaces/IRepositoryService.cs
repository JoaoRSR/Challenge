using App.Domain.Data;

namespace App.Application.Interfaces;

public interface IRepositoryService
{
    Task AddOrCreateUsersActivityCountToDatabaseAsync(IEnumerable<UsersActivityCount> data, CancellationToken cancellationToken = default);
}
