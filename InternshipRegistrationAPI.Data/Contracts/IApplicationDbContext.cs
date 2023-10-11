using Microsoft.Azure.Cosmos;

namespace InternshipRegistrationAPI.Data.Contracts
{
    public interface IApplicationDbContext
    {
        Database Database { get; }
    }
}