using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoviesAPI.Models.Configurations
{
    public class CinemaOfferConfig : IEntityTypeConfiguration<CinemaOffer>
    {
        public void Configure(EntityTypeBuilder<CinemaOffer> builder)
        {
            builder.Property(p => p.DiscountPercentage).HasPrecision(precision: 5, scale: 2);
            builder.Property(p => p.Begin).HasColumnType("Date");
            builder.Property(p => p.End).HasColumnType("Date");
        }
    }
}
