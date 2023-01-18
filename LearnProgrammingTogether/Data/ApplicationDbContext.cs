using LearnProgrammingTogether.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
	public DbSet<Group> Groups { get; set; }
	public DbSet<Technology> Technologies { get; set; }
	public DbSet<Adress> Adresses { get; set; }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{

	}
}