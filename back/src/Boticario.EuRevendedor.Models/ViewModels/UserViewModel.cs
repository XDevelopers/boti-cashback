using Boticario.EuRevendedor.Core.Data.MongoDb;

namespace Boticario.EuRevendedor.Models.ViewModels
{
    public class UserViewModel : EntityBase
    {
        public UserViewModel(User reseller)
        {
            Id = reseller.Id;
            CreatedAt = reseller.CreatedAt;
            UpdatedAt = reseller.UpdatedAt;

            Name = reseller.Name;
            Cpf = reseller.Cpf;
            Email = reseller.Email;
            Role = reseller.Role;
        }

        public string Name { get; set; }

        public string Cpf { get; set; }

        public string Email { get; set; }
        
        public string Role { get; set; }
    }
}
