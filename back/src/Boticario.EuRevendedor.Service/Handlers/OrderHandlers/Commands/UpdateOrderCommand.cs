using Boticario.EuRevendedor.Core.Extensions;
using Boticario.EuRevendedor.Core.Web;
using Flunt.Br.Extensions;
using Flunt.Validations;
using System;

namespace Boticario.EuRevendedor.Service.Handlers.OrderHandlers.Commands
{
    public class UpdateOrderCommand : Request<Response>
    {
        public string Id { get; set; }
        
        public string Code { get; set; }
        
        public decimal Value { get; set; }
        
        public DateTimeOffset Date { get; set; }

        public string Cpf { get; set; }

        public UpdateOrderCommand(
            string id,
            string code,
            decimal value,
            DateTimeOffset date,
            string cpf
        )
        {
            AddNotifications(new Contract()
                  .Requires()
                  .IsNotNullOrWhiteSpace(id, nameof(id), nameof(id).Required())
                  .IsNotNullOrWhiteSpace(code, nameof(code), nameof(code).Required())
                  .IsCpf(cpf, nameof(cpf), "Cpf informado é inválido!")
                  .IsGreaterThan(value, 0, nameof(value), nameof(value).Required())
                  .IsNotNull(date, nameof(date), nameof(date).Required())
              );

            if (Invalid)
                return;

            Id = id;
            Code = code;
            Value = value;
            Date = date;
            Cpf = cpf;
        }
    }
}
