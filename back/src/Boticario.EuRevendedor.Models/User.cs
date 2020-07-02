using Boticario.EuRevendedor.Core.Data.MongoDb;
using Boticario.EuRevendedor.Core.Security;
using Boticario.EuRevendedor.Interfaces.Models;

namespace Boticario.EuRevendedor.Models
{
    public class User : EntityBase, IUser
    {
        public User(string name, string cpf, string email, string password, string role)
        {
            Name = name;
            Cpf = cpf;
            Email = email;
            Password = new PasswordManager(password);
            Role = role;
            //UserType = role == "Administrador" ? EnumUserType.Administrator : EnumUserType.Reseller;
        }

        public string Name { get; set; }

        public string Cpf { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
