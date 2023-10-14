using InternshipRegistrationAPI.Core.Models;

namespace InternshipRegistrationAPI.Data.Contracts;

public interface IWorkflowRepository : IDistributeableDataRepository<Workflow>
{
}