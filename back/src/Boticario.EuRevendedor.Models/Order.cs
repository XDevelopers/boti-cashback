using Boticario.EuRevendedor.Core.Data.MongoDb;
using Boticario.EuRevendedor.Core.Web;
using Boticario.EuRevendedor.Interfaces.Enumerators;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Boticario.EuRevendedor.Models
{
    public class Order : EntityBase, IRequest<Response>
    {
        public Order(string code, decimal value, DateTimeOffset date, string cpf)
        {
            Code = code;
            Value = value;
            Date = date;
            Cpf = cpf;

            SetStatus();

            SetCashback();
        }

        public string Code { get; set; }

        public string Cpf { get; set; }

        public decimal Value { get; set; }

        public DateTimeOffset Date { get; set; }

        public decimal AppliedCashback { get; set; }

        public decimal ValueCashback { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EnumOrderStatus OrderStatus { get; set; }

        public void PrepareToUpdate(string code, decimal value, DateTimeOffset date, string cpf)
        {
            Code = code;
            Value = value;
            Date = date;
            Cpf = cpf;
            UpdatedAt = DateTimeOffset.UtcNow;

            SetStatus();

            SetCashback();
        }

        private void SetCashback()
        {
            if (Value < 1000M)
            {
                AppliedCashback = 10;
            }
            else if (Value <= 1500M)
            {
                AppliedCashback = 15;
            }
            else
            {
                AppliedCashback = 20;
            }

            ValueCashback = Math.Round( (AppliedCashback / 100) * Value, 2 );
        }

        private void SetStatus()
        {
            // HARDCODE Especificação do Sistema
            // TODO: Fazer pegar do Arquivo de Configuração e/ou Tabela de Configurações
            if (Cpf == "15350946056")
            {
                OrderStatus = EnumOrderStatus.Approved;
            }
            else
            {
                OrderStatus = EnumOrderStatus.OnChecking;
            }
        }
    }

}
