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
            var createDbTableWithSpecificDataCommand = new CreateDbTableWithUserDataHavingPostWithReactionsAndHistoryTagCommand();

            await _sender.Send(createDbTableWithSpecificDataCommand, new CancellationToken());
            Console.WriteLine($"Database table created succesfully.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Failed to create database table: {e.Message}");
        }

        Console.WriteLine("todo");
    }
}