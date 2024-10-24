using Contract.Domains.Interfaces;

namespace Contract.Domains.Abstracts
{
	public abstract class EntityBase<T>: IEntityBase<T>
	{
		public T Id { get; set; }
	}
}
