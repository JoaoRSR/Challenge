using App.Application.Data.Command;
using App.Application.Data.Queries;
using MediatR;

namespace secondApp;

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
            var createDbTableWithDataCommand = 
                new CreateDbTableWithUserDataCommand(
                    containingPostTag: "history", 
                    MinimumOfPostReactions: 1);

            await _sender.Send(createDbTableWithDataCommand, new CancellationToken());
            Console.WriteLine($"Database table created successfully.\n");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to create database table: {e.Message}\n");
        }

        try
        {
            var todosFromUsersQuery = new GetTodosFromUsersQuery(MinNumberOfPost: 3);

            var response = await _sender.Send(todosFromUsersQuery, new CancellationToken());

            Console.WriteLine("User todos: \n");
            foreach (var item in response)
            {
                Console.WriteLine(item.ToString());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to get todos: {e.Message}");
        }

        try
        {
            var postsFromUsersQuery = new GetPostsFromUsersQuery("mastercard");

            var response = await _sender.Send(postsFromUsersQuery, new CancellationToken());

            Console.WriteLine("\n\nUser posts: \n");
            foreach (var item in response)
            {
                Console.WriteLine(item.ToString());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to get posts: {e.Message}");
        }
    }
}