using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecommendationSystem.Domain.Entities;

namespace RecommendationSystem.Persistence.Configurations
{
    public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
        }

        public abstract void ConfigureEntityProperties(EntityTypeBuilder<T> builder);
    }
}
