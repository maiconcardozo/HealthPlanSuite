namespace Foundation.Base.Domain.Interface
{
    /// <summary>
    /// Base interface for all entities providing common properties.
    /// Compatible with Foundation.Base NuGet package interface.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        int Id { get; set; }
        
        /// <summary>
        /// Gets or sets whether the entity is active (soft delete flag).
        /// </summary>
        bool IsActive { get; set; }
        
        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        DateTime DtCreated { get; set; }
        
        /// <summary>
        /// Gets or sets the date and time when the entity was deleted (soft delete).
        /// </summary>
        DateTime? DtDeleted { get; set; }
        
        /// <summary>
        /// Gets or sets the date and time when the entity was last updated.
        /// </summary>
        DateTime? DtUpdated { get; set; }
        
        /// <summary>
        /// Gets or sets the user who created the entity.
        /// </summary>
        string CreatedBy { get; set; }
        
        /// <summary>
        /// Gets or sets the user who last updated the entity.
        /// </summary>
        string UpdatedBy { get; set; }
        
        /// <summary>
        /// Gets or sets the user who deleted the entity (soft delete).
        /// </summary>
        string DeletedBy { get; set; }
        
        /// <summary>
        /// Gets or sets the list of IDs for bulk operations.
        /// </summary>
        IEnumerable<int> LstId { get; set; }
        
        /// <summary>
        /// Gets or sets the start date for filtering created date range.
        /// </summary>
        DateTime? DtCreatedStart { get; set; }
        
        /// <summary>
        /// Gets or sets the end date for filtering created date range.
        /// </summary>
        DateTime? DtCreatedEnd { get; set; }
    }
}