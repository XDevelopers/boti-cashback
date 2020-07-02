using System;

namespace Boticario.EuRevendedor.Core.Data.MongoDb
{
    public interface IEntityBase<TKey>
    {
        TKey Id { get; set; }
    }

    public interface IEntityBase : IEntityBase<string>
    {
        DateTimeOffset? CreatedAt { get; set; }

        DateTimeOffset? UpdatedAt { get; set; }
    }
}
