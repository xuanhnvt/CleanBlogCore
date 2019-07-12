namespace Nhx.Core
{
    /// <summary>
    /// Base class for entities
    /// </summary>
    public abstract partial class BaseEntityWithTypedId<TKey>: IEntityId<TKey>
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public TKey Id { get; set; }
    }
}
