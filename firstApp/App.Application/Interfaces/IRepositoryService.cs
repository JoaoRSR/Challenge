using App.Domain.Data;

namespace App.Application.Interfaces;

public interface IRepositoryService
{
    Task CreateTableToDatabaseAsync(IEnumerable<UserInformation> userData, CancellationToken cancellationToken = default);
}
