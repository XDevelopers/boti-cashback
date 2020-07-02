using Boticario.EuRevendedor.Core.Data.MongoDb;
using Boticario.EuRevendedor.Interfaces.Enumerators;
using System;

namespace Boticario.EuRevendedor.Interfaces.Models
{
    public interface IOrder : IEntityBase
    {
        string Code { get; set; }

        decimal Value { get; set; }

        DateTime Date { get; set; }

        decimal AppliedCashback { get; set; }

        decimal ValueCashback { get; set; }

        EnumOrderStatus OrderStatus { get; set; }
    }
}
