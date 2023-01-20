using LearnProgrammingTogether.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
	public DbSet<Group> Groups { get; set; }
	public DbSet<Technology> Technologies { get; set; }
	public DbSet<Adress> Addresses { get; set; }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{

	}
}