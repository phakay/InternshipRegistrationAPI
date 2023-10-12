namespace InternshipRegistrationAPI.Data.Contracts
{
    public interface ICosmosDbDocument
    {
        string Id { get; set; }
        string PartitionKey { get; }
    }
}
