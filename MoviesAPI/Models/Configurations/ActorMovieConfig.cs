using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoviesAPI.Models.Configurations
{
    public class ActorMovieConfig : IEntityTypeConfiguration<ActorMovie>
    {
        public void Configure(EntityTypeBuilder<ActorMovie> builder)
        {
            builder.HasKey(p => new { p.ActorId, p.MovieId });
            builder.Property(p => p.Character).HasMaxLength(150);
        }
    }
}
