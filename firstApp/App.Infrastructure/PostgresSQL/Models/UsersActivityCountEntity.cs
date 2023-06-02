using System.ComponentModel.DataAnnotations;

namespace App.Infrastructure.PostgresSQL.Models;

public class UsersActivityCountEntity
{
    [Key]
    public int UserId { get; set; }

    public int TodosNumber { get; set; }

    public int PostsNumber { get; set; }
}
