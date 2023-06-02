using App.Application.Interfaces;
using App.Domain.Data.Requests;
using MediatR;

namespace App.Application.Data.Command;

public record CreateDbTableWithUserDataCommand(string containingPostTag = "", int MinimumOfPostReactions = 0)
 : IRequest<Unit>;

public class CreateDbTableWithUserDataHandler : IRequestHandler<CreateDbTableWithUserDataCommand, Unit>
{
    private readonly IDataService _dataService;
    private readonly IRepositoryService _repositoryService;

    public CreateDbTableWithUserDataHandler(
        IRepositoryService repositoryService,
        IDataService dataService)
    {
        _repositoryService = repositoryService;
        _dataService = dataService;
    }

    public async Task<Unit> Handle(CreateDbTableWithUserDataCommand request, CancellationToken cancellationToken)
    {
        //1 - search posts with reactions and having history tag:
        var posts = await _dataService.GetAllPostsTagsAndReactionsAsync(cancellationToken);
        var userIdsFromTodos = await _dataService.GetAllUserIDsFromTodosAsync(cancellationToken);

        var selectedUserIds = posts
            .Where(p => p.Reactions >= request.MinimumOfPostReactions)
            .Where(p => string.IsNullOrEmpty(request.containingPostTag) || p.Tags.Contains(request.containingPostTag))
            .Select(x => x.UserId)
            .Distinct();

        //2 - Prepare data to be written to db
        var allUsersActivity = new List<UsersActivityCount>();

        foreach (var userId in selectedUserIds)
        {
            var postsNumber = posts.Count(p => p.UserId == userId);
            var todosNumber = userIdsFromTodos.Count(p => p == userId);

            var userActivity = new UsersActivityCount()
            {
                UserId = userId,
                NumberOfPosts = postsNumber,
                NumberofTodos = todosNumber,
            };

            allUsersActivity.Add(userActivity);
        }

        //4 - write to db
        await _repositoryService.AddOrCreateUsersActivityCountToDatabaseAsync(allUsersActivity);

        return Unit.Value;
    }
}
