using App.Application.Interfaces;
using App.Domain.Data.Requests;
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
        var request = data.Select(item => new UsersActivityCountEntity()
        {
            UserId = item.UserId,
            PostsNumber = item.NumberOfPosts,
            TodosNumber = item.NumberofTodos
        });

        // Option 1 - we need to delete all entries and enter the new ones: 
        //var existingItemsToDelete = _context.UsersActivityCount.ToList();
        //_context.UsersActivityCount.RemoveRange(existingItemsToDelete);
        //await _context.UsersActivityCount.AddRangeAsync(request, cancellationToken);
        //await _context.SaveChangesAsync(cancellationToken);

        // Option 2 - we just add the new values and keep what exists in the db
        var existingItems = _context.UsersActivityCount.ToDictionary(x => x.UserId);

        //if it's not expected the existing data on DB to change much, we could create also an hashset just to ignore existing entries

        foreach (var item in request)
        {
            if (existingItems.ContainsKey(item.UserId))
            {
                existingItems[item.UserId] = item;
            }
            else
            {
                await _context.UsersActivityCount.AddAsync(item);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddOrCreatePostsToDatabaseAsync(IEnumerable<PostsToSave> data, CancellationToken cancellationToken = default)
    {
        var request = data.Select(item => new PostsEntity()
        {
            Id = item.Id,
            UserId = item.UserId,
            UserName = item.UserName,
            HasFrenchTag = item.HasFrenchTag,
            HasFictionTag = item.HasFictionTag,
            HasMoreThanTwoReactions = item.HasMoreThanTwoReactions,
        });

        // Option 1 - we need to delete all entries and enter the new ones: 
        //var existingItemsToDelete = _context.Posts.ToList();
        //_context.Posts.RemoveRange(existingItemsToDelete);
        //await _context.Posts.AddRangeAsync(request, cancellationToken);
        //await _context.SaveChangesAsync(cancellationToken);

        // Option 2 - we just add the new values and keep what exists in the db
        var existingItems = _context.Posts.ToDictionary(x => x.Id);

        //if it's not expected the existing data on DB to change much, we could create also an hashset just to ignore existing entries

        var itemsToAdd = new LinkedList<PostsEntity>();

        foreach (var item in request)
        {
            if (existingItems.ContainsKey(item.Id))
            {
                existingItems[item.Id] = item;
            }
            else
            {
                await _context.Posts.AddAsync(item);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
