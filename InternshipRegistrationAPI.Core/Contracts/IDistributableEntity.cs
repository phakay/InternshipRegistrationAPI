namespace InternshipRegistrationAPI.Core.Contracts
{
    public interface IDistributableEntity
    {
        string Id { get; set; }
        string PartitionKey { get; }
    }
}
