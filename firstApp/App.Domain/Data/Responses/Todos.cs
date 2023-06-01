using System.Text;

namespace App.Domain.Data.Responses;

public class Todos
{
    public int Id { get; set; }
    public string Todo { get; set; }
    public bool Completed { get; set; }
    public int UserId { get; set; }

    public override string ToString()
    {
        var completedText = Completed ? "completed" : "to be done";
        return $"UserId {UserId} -> '{Todo}' - state: {completedText}.";
    }
}
