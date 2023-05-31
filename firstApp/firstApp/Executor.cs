using App.Application.Data.Queries;
using App.Application.Database.Command;
using MediatR;

namespace firstApp;

public class Executor
{
    private readonly ISender _sender;

    public Executor(ISender sender)
    {
        _sender = sender ?? throw new ArgumentNullException(nameof(sender));
    }

    public async Task Execute(string[] args)
    {
        try
        {
            var createDbTableWithSpecificDataCommand = 
                new CreateDbTableWithUserDataHavingPostWithReactionsAndHistoryTagCommand(
                    containingPostTag: "history", 
                    MinimumOfPostReactions: 1);

            await _sender.Send(createDbTableWithSpecificDataCommand, new CancellationToken());
            Console.WriteLine($"Database table created successfully.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to create database table: {e.Message}");
        }

        try
        {
            var todosFromUsersQuery = new GetTodosFromUsersQuery(MinNumberOfPost: 3);

            var response = await _sender.Send(todosFromUsersQuery, new CancellationToken());
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to get todos: {e.Message}");
        }
    }
}