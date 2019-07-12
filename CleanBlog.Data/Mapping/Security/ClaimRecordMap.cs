using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nhx.Core.Entities.Security;

namespace CleanBlog.Data.Mapping.Security
{
    /// <summary>
    /// Represents a permission record mapping configuration
    /// </summary>
    public partial class ClaimRecordMap : EntityTypeConfiguration<ClaimRecord>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<ClaimRecord> builder)
        {
            builder.ToTable(nameof(ClaimRecord));
            builder.HasKey(record => record.Id);

            builder.Property(record => record.Name).IsRequired();
            builder.Property(record => record.SystemName).HasMaxLength(255).IsRequired();

            //builder.HasMany(claim => claim.RoleClaims)
            //    .WithOne(roleClaim => roleClaim.ClaimRecord)
            //    .HasForeignKey(roleClaim => roleClaim.ClaimRecordId)
            //.IsRequired();

            //builder.HasMany(claim => claim.UserClaims)
            //    .WithOne(userClaim => userClaim.ClaimRecord)
            //    .HasForeignKey(userClaim => userClaim.ClaimRecordId)
            //    .IsRequired();

            base.Configure(builder);
        }

        #endregion
    }
}