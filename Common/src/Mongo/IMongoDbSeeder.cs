using System.Threading.Tasks;

namespace Common.Mongo
{
    public interface IMongoDbSeeder
    {
        Task SeedAsync();
    }
}