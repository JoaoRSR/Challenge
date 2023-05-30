using App.Application.Interfaces;
using App.Domain.Data;
using MediatR;

namespace App.Application.Data.Queries;

public record GetTodosFromUsersWithMoreThanSearchCriteriaQuery()
 : IRequest<IEnumerable<Todos>>;

//public class GetUserDataHavingPostWithReactionsAndHistoryTagHandler : IRequestHandler<GetUserDataHavingPostWithReactionsAndHistoryTagQuery, IEnumerable<UserData>>
//{
//    private readonly IDataService _dataService;

//    public GetUserDataHavingPostWithReactionsAndHistoryTagHandler(IDataService dataService)
//    {
//        _dataService = dataService;
//    }

//    public async Task<IEnumerable<UserData>> Handle(GetUserDataHavingPostWithReactionsAndHistoryTagQuery request, CancellationToken cancellationToken)
//    {
//        IEnumerable<UserData> response;

//        try
//        {
//            response = await _dataService.GetUserDataHavingPostWithReactionsAndHistoryTagAsync(cancellationToken);
//        }
//        catch (Exception e)
//        {
//            throw new Exception($"Failed to retrive user data at {nameof(GetUserDataHavingPostWithReactionsAndHistoryTagHandler)}: {e.Message}");
//        }

//        return response;
//    }
//}