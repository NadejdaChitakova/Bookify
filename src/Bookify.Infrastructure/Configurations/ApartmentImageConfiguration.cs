using Bookify.Domain.Apartments;
using Bookify.Domain.AttachedFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure.Configurations
{
    internal sealed class ApartmentImageConfiguration : IEntityTypeConfiguration<ApartmentImage>
    {
        public void Configure(EntityTypeBuilder<ApartmentImage> builder)
        {
            builder.ToTable("apartment_image");

            builder.HasKey(image => image.Id);

            builder.Property(apartment => apartment.FileContent)
                .HasMaxLength(2000)
                .HasConversion(name => name.Value, value => new FileContent(value));

            builder.Property(apartment => apartment.Extension)
                .HasMaxLength(2000)
                .HasConversion(name => name.Value, value => new Extension(value));

            builder.HasOne<Apartment>()
                .WithMany()
                .HasForeignKey(review => review.ApartmentId);

        }
    }
}
