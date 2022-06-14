using Microsoft.EntityFrameworkCore;
using MoviesAPI.Models;
using MoviesAPI.Models.Seeders;
using System.Reflection;

namespace MoviesAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            InitialSeeder.Seed(modelBuilder);
        }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<CinemaOffer> CinemaOffers { get; set; }

        public DbSet<CinemaHall> CinemaHalls { get; set; }

        public DbSet<ActorMovie> ActorMovie { get; set; }
    }
}
