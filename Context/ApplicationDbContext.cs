using GrafanaPrometheusTest.Models;
using Microsoft.EntityFrameworkCore;

namespace GrafanaPrometheusTest.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }
}
