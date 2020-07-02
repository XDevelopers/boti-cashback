using System;

namespace Boticario.EuRevendedor.Core.Data.MongoDb
{
    public class EntityBase : IEntityBase
    {
        public string Id { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public EntityBase()
        {
            //Id = ObjectId.GenerateNewId();
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
