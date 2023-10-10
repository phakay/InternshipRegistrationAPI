using InternshipRegistrationAPI.Core.Models;
using InternshipRegistrationAPI.Data.Contracts;
using Newtonsoft.Json;

namespace InternshipRegistrationAPI.Data.DataModels
{
    public class ProgramData : Program, ICosmosDbDocument
    {
        [JsonIgnore]
        public string PartitionKey => Id;
    }

}
