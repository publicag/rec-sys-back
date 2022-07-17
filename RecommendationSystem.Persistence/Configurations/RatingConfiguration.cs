using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecommendationSystem.Domain.Entities;

namespace RecommendationSystem.Persistence.Configurations
{
    public class RatingConfiguration : EntityConfiguration<Rating>
    {
        public override void ConfigureEntityProperties(EntityTypeBuilder<Rating> builder)
        {
            builder.Property(p => p.Rate)
                .IsRequired(true)
                .HasPrecision(1, 1);
            builder.Property(p => p.UserId)
                .IsRequired(true);
        }
    }
}
