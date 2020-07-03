using Boticario.EuRevendedor.Core.Data.MongoDb;

namespace Boticario.EuRevendedor.Interfaces.Models
{
    public interface IUser : IEntityBase
    {
        string Name { get; set; }

        string Cpf { get; set; }

        string Email { get; set; }

        string Password { get; set; }

        string Role { get; set; }

        //EnumUserType UserType { get;set; }
    }
}
