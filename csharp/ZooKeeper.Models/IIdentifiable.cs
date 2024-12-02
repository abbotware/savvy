namespace Savvy.ZooKeeper.Models
{
    /// <summary>
    /// Interface for an identifiable object
    /// </summary>
    public interface IIdentifiable<TKey>
    {
        /// <summary>
        /// Gets the Id
        /// </summary>
        TKey Id { get; }
    }
}