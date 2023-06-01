using System.Text;

namespace App.Domain.Data.Responses;

public class Posts
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
    public string[] Tags { get; set; }
    public int Reactions { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Title: {Title}");
        sb.AppendLine($"Content: {Body}");
        sb.AppendLine($"Tags: {string.Join(", ", Tags)}");
        sb.AppendLine($"Number of reactions: {Reactions}");
        sb.AppendLine($"By UserId: {UserId}");

        return sb.ToString();
    }
}
