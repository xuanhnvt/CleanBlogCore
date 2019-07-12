using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nhx.Core.Entities.Security;

namespace CleanBlog.Data.Mapping.Security
{
    /// <summary>
    /// Represents a permission record mapping configuration
    /// </summary>
    public partial class PermissionRecordMap : EntityTypeConfiguration<PermissionRecord>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<PermissionRecord> builder)
        {
            builder.ToTable(nameof(PermissionRecord));
            builder.HasKey(record => record.Id);

            builder.Property(record => record.Name).IsRequired();
            builder.Property(record => record.SystemName).HasMaxLength(255).IsRequired();
            builder.Property(record => record.Category).HasMaxLength(255).IsRequired();

            base.Configure(builder);
        }

        #endregion
    }
}