using System.ComponentModel.DataAnnotations;

namespace App.Infrastructure.PostgresSQL.Configurations;

public class PgConfiguration
{
    public const string SectionName = "PG_Database";

    [Required]
    public string ConnectionString { get; init; }
}