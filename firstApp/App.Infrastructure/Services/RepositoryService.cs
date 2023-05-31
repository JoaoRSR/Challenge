using App.Application.Interfaces;
using App.Domain.Data;
using App.Infrastructure.PostgresSQL.Models;

namespace App.Infrastructure.Services;
public class RepositoryService : IRepositoryService
{
    private readonly ChallengeDbContext _context;

    public RepositoryService(ChallengeDbContext context)
    {
        _context = context;
    }

    public async Task AddOrCreateUsersActivityCountToDatabaseAsync(IEnumerable<UsersActivityCount> data, CancellationToken cancellationToken = default)
    {
        var existingUsers = _context.UsersActivityCount.ToList().ToDictionary(x => x.UserId);

        var usersToUpdate = new List<UsersActivityCountEntity>();
        var usersToAdd = new List<UsersActivityCountEntity>();

        foreach (var item in data)
        {
            if (existingUsers.ContainsKey(item.Id))
            {
                existingUsers[item.Id].TodosNumber = item.NumberofTodos;
                existingUsers[item.Id].PostsNumber = item.NumberOfPosts;
            }
            else
            {
                usersToAdd.Add(new UsersActivityCountEntity()
                {
                    UserId = item.Id,
                    PostsNumber = item.NumberOfPosts,
                    TodosNumber = item.NumberOfPosts
                });
            }
        }

        _context.UsersActivityCount.AddRange(usersToAdd);
        _context.SaveChanges();
    }
}
