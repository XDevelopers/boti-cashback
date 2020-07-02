using System;

namespace Boticario.EuRevendedor.Core.Data.LiteDb
{
    public interface IEntityBase
    {
        DateTimeOffset? CreatedTime { get; set; }

        string Id { get; set; }

        DateTimeOffset? ModifiedTime { get; set; }
    }
}