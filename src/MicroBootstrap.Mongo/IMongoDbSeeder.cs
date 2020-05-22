using System.Threading.Tasks;

namespace MicroBootstrap.Mongo
{
    public interface IMongoDbSeeder
    {
        Task SeedAsync();
    }
}