using App.Application.Interfaces;
using App.Domain.Data;
using MediatR;

namespace App.Application.Database.Command;

public record CreateDbTableWithUserDataHavingPostWithReactionsAndHistoryTagCommand()
 : IRequest<Unit>;

public class CreateDbTableWithUserDataHavingPostWithReactionsAndHistoryTagHandler : IRequestHandler<CreateDbTableWithUserDataHavingPostWithReactionsAndHistoryTagCommand, Unit>
{
    private readonly IDataService _dataService;
    private readonly IRepositoryService _repositoryService;

    public CreateDbTableWithUserDataHavingPostWithReactionsAndHistoryTagHandler(
        //IRepositoryService repositoryService, 
        IDataService dataService)
    {
        //_repositoryService = repositoryService;
        _dataService = dataService;
    }

    public async Task<Unit> Handle(CreateDbTableWithUserDataHavingPostWithReactionsAndHistoryTagCommand request, CancellationToken cancellationToken)
    {
        IEnumerable<Posts> userDataResponse;

        try
        {
            userDataResponse = await _dataService.GetAllPostsAsync(cancellationToken);
            var userDataResponse2 = await _dataService.GetAllTodosAsync(cancellationToken);
            var userDataResponse3 = await _dataService.GetAllUsersWithCardTypeAsync("mastercard", cancellationToken);
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to retrive user data at {nameof(CreateDbTableWithUserDataHavingPostWithReactionsAndHistoryTagHandler)}: {e.Message}");
        }

        try
        {
            //await _repositoryService.CreateTableToDatabaseAsync(userDataResponse, cancellationToken);
            ;
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to create table to database at {nameof(CreateDbTableWithUserDataHavingPostWithReactionsAndHistoryTagHandler)}: {e.Message}.");
        }

        return Unit.Value;
    }
}
