using Boticario.EuRevendedor.Core.Security;
using Boticario.EuRevendedor.Data.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Repository.MongoDb
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        protected MongoDbContext Context { get; }

        public RefreshTokenRepository(MongoDbContext context)
        {
            Context = context;
        }

        public async Task<RefreshToken> Get(string refreshToken)
        {
            return await Context.GetAsync(Builders<RefreshToken>.Filter.Eq(c => c.Token, refreshToken));
        }

        public async Task Save(RefreshToken refreshToken)
        {
            await Context.DeleteAsync(Builders<RefreshToken>.Filter.Eq(c => c.Email, refreshToken.Email));

            refreshToken.Id = ObjectId.GenerateNewId().ToString();
            await Context.InsertAsync(refreshToken);
        }
    }
}
