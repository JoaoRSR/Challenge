using App.Application.Interfaces;
using App.Domain.Data.Requests;
using MediatR;

namespace App.Application.Data.Command;

public record CreateDbTableWithPostDataCommand()
 : IRequest<Unit>;

public class CreateDbTableWithPostDataHandler : IRequestHandler<CreateDbTableWithPostDataCommand, Unit>
{
    private readonly IDataService _dataService;
    private readonly IRepositoryService _repositoryService;

    public CreateDbTableWithPostDataHandler(
        IRepositoryService repositoryService,
        IDataService dataService)
    {
        _repositoryService = repositoryService;
        _dataService = dataService;
    }

    public async Task<Unit> Handle(CreateDbTableWithPostDataCommand request, CancellationToken cancellationToken)
    {
        //1 - get all posts:
        var posts = await _dataService.GetAllPostsTagsAndReactionsAsync(cancellationToken);

        //2 - get posts userId corresponding UserName and Prepare data to be written to db
        var postsToSave = new List<PostsToSave>();

        var userNames = await _dataService.GetUserNameFromUsersAsync(cancellationToken);

        var uniqueUserIds = posts.Select(p => p.UserId).Distinct().OrderBy(i => i);

        foreach (var item in uniqueUserIds)
        {
            string userName = userNames.FirstOrDefault(u => u.Id == item)?.UserName ?? "";

            postsToSave.AddRange(
                posts.Where(p => p.UserId == item)
                     .Select(p => new PostsToSave()
                        {
                            Id = p.Id,
                            UserId = p.UserId,
                            UserName = userName,
                            HasFictionTag = p.Tags.Contains("fiction"), //in this case it doesn't make sense to have this as input because this is part of the database
                            HasFrenchTag = p.Tags.Contains("french"),
                            HasMoreThanTwoReactions = p.Reactions > 2,
                        }));
        }

        //4 - write to db
        await _repositoryService.AddOrCreatePostsToDatabaseAsync(postsToSave);

        return Unit.Value;
    }
}
