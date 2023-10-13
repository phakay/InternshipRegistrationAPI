namespace InternshipRegistrationAPI.Core.Contracts
{
    public interface IDistributableEntity
    {
        string Id { get; }
        string PartitionKey { get; }
    }
}
