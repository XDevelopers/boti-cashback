using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Interfaces.Service
{
    public interface IBoticarioApiService
    {
        Task<decimal?> Cashback(string cpf);
    }
}
