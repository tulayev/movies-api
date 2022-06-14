using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoviesAPI.Models.Configurations
{
    public class MovieConfig : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(p => p.Title).HasMaxLength(255).IsRequired();
            builder.Property(p => p.ReleaseDate).HasColumnType("Date");
            builder.Property(p => p.Poster).HasMaxLength(500).IsUnicode(false);
        }
    }
}
