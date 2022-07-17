using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecommendationSystem.Domain.Entities;

namespace RecommendationSystem.Persistence.Configurations
{
    public class MovieConfiguration : EntityConfiguration<Movie>
    {
        public override void ConfigureEntityProperties(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(p => p.Title)
                .IsRequired(true)
                .HasMaxLength(400);
            builder.HasMany(n => n.Genres);
        }
    }
}
