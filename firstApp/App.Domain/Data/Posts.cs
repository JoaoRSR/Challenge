namespace App.Domain.Data;

public class Posts
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
    public string[] Tags { get; set; }
    public int Reactions { get; set; }
}
