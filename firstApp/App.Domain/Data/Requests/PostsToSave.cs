namespace App.Domain.Data.Requests;

public class PostsToSave
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string UserName { get; set; }

    public bool HasFrenchTag { get; set; }

    public bool HasFictionTag { get; set; }

    public bool HasMoreThanTwoReactions { get; set; }
}
