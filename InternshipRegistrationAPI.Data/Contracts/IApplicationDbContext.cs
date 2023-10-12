using Microsoft.Azure.Cosmos;

namespace InternshipRegistrationAPI.Data.Contracts
{
    public interface IApplicationDbContext : IDisposable
    {
        Database Database { get; }
    }
}