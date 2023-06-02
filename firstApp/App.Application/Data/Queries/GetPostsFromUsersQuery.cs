using App.Application.Interfaces;
using App.Domain.Data.Responses;
using MediatR;

namespace App.Application.Data.Queries;

public record GetPostsFromUsersQuery(string CreditCardType)
 : IRequest<IEnumerable<Posts>>;

public class GetPostsFromUsersHandler : IRequestHandler<GetPostsFromUsersQuery, IEnumerable<Posts>>
{
    private readonly IDataService _dataService;

    public GetPostsFromUsersHandler(IDataService dataService)
    {
        _dataService = dataService;
    }

    public async Task<IEnumerable<Posts>> Handle(GetPostsFromUsersQuery request, CancellationToken cancellationToken)
    {
        var response = new List<Posts>();

        // 1 - get users that uses “mastercard” as their cardtype:
        var userIdsFromTodos = await _dataService.GetAllUsersWithCardTypeAsync(request.CreditCardType, cancellationToken);
        var uniqueUserIds = userIdsFromTodos.Select(u => u.Id).Distinct();

        // 2 - get those users posts
        foreach (var userId in uniqueUserIds)
        {
            response.AddRange(await _dataService.GetAllPostsAsync(userId, cancellationToken));
        }

        return response;
    }
}