using System.ComponentModel.DataAnnotations;

namespace App.Infrastructure.PostgresSQL.Models;

public class PostsEntity
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    public string UserName { get; set; }

    public bool HasFrenchTag { get; set; }

    public bool HasFictionTag { get; set; }

    public bool HasMoreThanTwoReactions { get; set; }
}
