namespace App.Domain.Data.Requests;

public class UsersActivityCount
{
    public int UserId { get; set; }
    public int NumberOfPosts { get; set; }
    public int NumberofTodos { get; set; }
}
