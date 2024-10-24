using Contract.Domains.Interfaces;

namespace Contract.Domains.Abstracts
{
	public abstract class EntityAuditBase<T> : EntityBase<T>, IAuditable
	{
		public DateTimeOffset CreatedDate { get; set; }
		public DateTimeOffset LastModifiedDate { get; set; }
	}
}
