using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sem2.riverStations.urbanec.ait4.Services;
using sem2.riverStations.urbanec.ait4.StationInfomrations;

namespace sem2.riverStations.urbanec.ait4;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Station> Stations { get; set; }
    public DbSet<HistoryValues> HistoryValues { get; set; }
    public DbSet<TokenSetUp> TokenSetUp {  get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
    }
}
