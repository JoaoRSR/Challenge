﻿using App.Application.Interfaces;
using App.Domain.Data.Responses;
using MediatR;

namespace App.Application.Data.Queries;

public record GetTodosFromUsersQuery(int MinNumberOfPost = 0)
 : IRequest<IEnumerable<Todos>>;

public class GetTodosFromUsersHandler : IRequestHandler<GetTodosFromUsersQuery, IEnumerable<Todos>>
{
    private readonly IDataService _dataService;

    public GetTodosFromUsersHandler(IDataService dataService)
    {
        _dataService = dataService;
    }

    public async Task<IEnumerable<Todos>> Handle(GetTodosFromUsersQuery request, CancellationToken cancellationToken)
    {
        var response = new List<Todos>();

        // 1 - get users with more than 2 posts:
        var userIdsFromPosts = await _dataService.GetAllUserIdFromPostsAsync(cancellationToken);
        var uniqueUserIds = userIdsFromPosts.Select(p => p).Distinct();

        // 2 - get those users todos
        foreach (var item in uniqueUserIds)
        {
            if (userIdsFromPosts.Count(u => u == item) > request.MinNumberOfPost)
            {
                response.AddRange(await _dataService.GetAllTodosAsync(item, cancellationToken));
            }
        }

        return response;
    }
}