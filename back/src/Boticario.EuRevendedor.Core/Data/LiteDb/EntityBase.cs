using System;

namespace Boticario.EuRevendedor.Core.Data.LiteDb
{
    public abstract class EntityBase : IEntityBase
    {
        public string Id { get; set; }

        public DateTimeOffset? CreatedTime { get; set; }

        public DateTimeOffset? ModifiedTime { get; set; }
    }
}
