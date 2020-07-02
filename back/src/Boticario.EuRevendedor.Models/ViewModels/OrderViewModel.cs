using Boticario.EuRevendedor.Interfaces.Enumerators;
using System;

namespace Boticario.EuRevendedor.Models.ViewModels
{
    public class OrderViewModel
    {
        public string Code { get; set; }

        public string Cpf { get; set; }

        public decimal Value { get; set; }

        public DateTimeOffset Date { get; set; }

        public decimal AppliedCashback { get; set; }

        public decimal ValueCashback { get; set; }

        public EnumOrderStatus OrderStatus { get; set; }
    }
}
