using Boticario.EuRevendedor.Core.Security;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Repository.MongoDb
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> Get(string refreshToken);

        Task Save(RefreshToken refreshToken);
    }
}
