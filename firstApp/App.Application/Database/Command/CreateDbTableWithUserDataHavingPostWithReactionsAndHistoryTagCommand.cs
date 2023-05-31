using App.Application.Interfaces;
using App.Domain.Data;
using MediatR;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace App.Application.Database.Command;

public record CreateDbTableWithUserDataHavingPostWithReactionsAndHistoryTagCommand(string containingPostTag = "", int MinimumOfPostReactions = 0)
 : IRequest<Unit>;

public class CreateDbTableWithUserDataHavingPostWithReactionsAndHistoryTagHandler : IRequestHandler<CreateDbTableWithUserDataHavingPostWithReactionsAndHistoryTagCommand, Unit>
{
    private readonly IDataService _dataService;
    private readonly IRepositoryService _repositoryService;

    public CreateDbTableWithUserDataHavingPostWithReactionsAndHistoryTagHandler(
        IRepositoryService repositoryService,
        IDataService dataService)
    {
        _repositoryService = repositoryService;
        _dataService = dataService;
    }

    public async Task<Unit> Handle(CreateDbTableWithUserDataHavingPostWithReactionsAndHistoryTagCommand request, CancellationToken cancellationToken)
    {
        //1 - search posts with reactions and having history tag:
        var posts = await _dataService.GetAllPostsAsync(cancellationToken);
        var userIdsFromTodos = await _dataService.GetAllUserIDsFromTodosAsync(cancellationToken);

        var selectedUserIds = posts
            .Where(p => p.Reactions >= request.MinimumOfPostReactions)
            .Where(p => string.IsNullOrEmpty(request.containingPostTag) ? true : p.Tags.Contains(request.containingPostTag))
            .Select(x => x.UserId)
            .Distinct();

        var allUsersActivity = new List<UsersActivityCount>();

        //2 - Prepare data to be written to db
        foreach (var userId in selectedUserIds)
        {
            var postsNumber = posts.Count(p => p.UserId == userId);
            var todosNumber = userIdsFromTodos.Count(p => p.Id == userId);

            var userActivity = new UsersActivityCount()
            {
                Id = userId,
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
