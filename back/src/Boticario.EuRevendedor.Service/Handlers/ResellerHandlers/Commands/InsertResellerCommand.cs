using Boticario.EuRevendedor.Core.Extensions;
using Boticario.EuRevendedor.Core.Web;
using Flunt.Br.Extensions;
using Flunt.Validations;

namespace Boticario.EuRevendedor.Service.Handlers.ResellerHandlers.Commands
{
    public class InsertResellerCommand : Request<Response>
    {
        public string Name { get; set; }

        public string Cpf { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public InsertResellerCommand(
            string name,
            string cpf,
            string email,
            string password,
            string role
        )
        {
            AddNotifications(new Contract()
                  .Requires()
                  .IsNotNullOrWhiteSpace(name, nameof(name), nameof(name).Required())
                  .IsCpf(cpf, nameof(cpf), "Cpf informado é inválido!")
                  .IsNotNullOrWhiteSpace(email, nameof(email), nameof(email).Required())
                  .IsEmail(email, nameof(email), "E-mail informado é inválido!")
                  .IsNotNullOrWhiteSpace(password, nameof(password), nameof(password).Required())
                  .IsNotNullOrWhiteSpace(role, nameof(role), nameof(role).Required())
              );

            if (Invalid)
                return;

            Name = name.Trim();
            Cpf = cpf.Trim().NumberOnly();
            Email = email.Trim();
            Password = password;
            Role = role;
        }
    }
}
