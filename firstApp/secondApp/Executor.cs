using App.Application.Data.Command;
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
            var createDbTableWithPostCommand =
                new CreateDbTableWithPostDataCommand();

            await _sender.Send(createDbTableWithPostCommand, new CancellationToken());
            Console.WriteLine($"Database table created successfully.\n");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to create database table: {e.Message}\n");
        }
    }
}