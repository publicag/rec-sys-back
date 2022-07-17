using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecommendationSystem.Domain.Entities;

namespace RecommendationSystem.Persistence.Configurations
{
    public class GenreConfiguration : EntityConfiguration<Genre>
    {
        public override void ConfigureEntityProperties(EntityTypeBuilder<Genre> builder)
        {
            builder.Property(n => n.Name)
                .IsRequired(true);
        }
    }
}
