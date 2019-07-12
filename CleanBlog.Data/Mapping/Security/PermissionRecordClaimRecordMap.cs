using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nhx.Core.Entities.Security;

namespace CleanBlog.Data.Mapping.Security
{
    /// <summary>
    /// Represents a permission record-customer role mapping configuration
    /// </summary>
    public partial class PermissionRecordClaimRecordMap : EntityTypeConfiguration<PermissionRecordClaimRecordMapping>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<PermissionRecordClaimRecordMapping> builder)
        {
            builder.ToTable("PermissionRecordClaimRecordMapping");
            builder.HasKey(mapping => mapping.Id);

            builder.Property(mapping => mapping.PermissionRecordId).HasColumnName("PermissionRecord_Id");
            builder.Property(mapping => mapping.ClaimRecordId).HasColumnName("ClaimRecord_Id");

            builder.HasOne(mapping => mapping.ClaimRecord)
                .WithMany(role => role.PermissionRecordClaimRecordMappings)
                .HasForeignKey(mapping => mapping.ClaimRecordId)
                .IsRequired();

            builder.HasOne(mapping => mapping.PermissionRecord)
                .WithMany(record => record.PermissionRecordClaimRecordMappings)
                .HasForeignKey(mapping => mapping.PermissionRecordId)
                .IsRequired();


            base.Configure(builder);
        }

        #endregion
    }
}