using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace AhmadBase.Inferastracter.Datas.Entities
{


    public interface IAggregateRoot
    {
    }

    public interface ITxRequest
    {
    }

    public abstract class EntityBase
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; protected set; }
        public bool IsDeleted { get; set; } = false;
    }


    public abstract class BaseEntityTypeConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<TBase> entityTypeBuilder)
        {
            entityTypeBuilder.Property<byte[]>("RowVersion").IsRowVersion();
        }
    }
}
