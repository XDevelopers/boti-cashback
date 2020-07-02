using LiteDB;

namespace Boticario.EuRevendedor.Data.LiteDb
{
    public interface ILiteDbContext
    {
        LiteDatabase Database { get; }
    }
}
