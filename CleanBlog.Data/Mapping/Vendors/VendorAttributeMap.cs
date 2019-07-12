using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nhx.Core.Entities.Vendors;

namespace CleanBlog.Data.Mapping.Vendors
{
    /// <summary>
    /// Represents an vendor attribute mapping configuration
    /// </summary>
    public partial class VendorAttributeMap : EntityTypeConfiguration<VendorAttribute>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<VendorAttribute> builder)
        {
            builder.ToTable(nameof(VendorAttribute));
            builder.HasKey(attribute => attribute.Id);

            builder.Property(attribute => attribute.Name).HasMaxLength(400).IsRequired();

            base.Configure(builder);
        }

        #endregion
    }
}