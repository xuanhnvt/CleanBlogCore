using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nhx.Core.Entities.Blogs;

namespace CleanBlog.Data.Mapping.Blogs
{
    /// <summary>
    /// Represents a category mapping configuration
    /// </summary>
    public partial class BlogCategoryMap : EntityTypeConfiguration<BlogCategory>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<BlogCategory> builder)
        {
            builder.ToTable(nameof(BlogCategory));
            builder.HasKey(category => category.Id);

            builder.Property(category => category.Name).HasMaxLength(400).IsRequired();
            builder.Property(category => category.MetaKeywords).HasMaxLength(400);
            builder.Property(category => category.MetaTitle).HasMaxLength(400);

            base.Configure(builder);
        }

        #endregion
    }
}