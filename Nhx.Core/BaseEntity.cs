namespace Nhx.Core
{
    /// <summary>
    /// Base class for entities
    /// </summary>
    public abstract partial class BaseEntity: BaseEntityWithTypedId<int>, IEntityId<int>
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        //public int Id { get; set; }
    }
}
