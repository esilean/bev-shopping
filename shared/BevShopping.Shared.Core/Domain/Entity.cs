namespace BevShopping.Shared.Core.Domain
{
    public abstract class Entity<T>
    {
        public virtual T Id { get; protected set; }
    }
}