using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternshipRegistrationAPI.Data.Contracts
{
    public interface ICosmosDbDocument
    {
        string Id { get; set; }
        string PartitionKey { get; }
    }
}
