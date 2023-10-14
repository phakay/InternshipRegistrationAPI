using InternshipRegistrationAPI.Data.Contracts;
using InternshipRegistrationAPI.Core.Models;

namespace InternshipRegistrationAPI.Data.Repositories
{
    public class FormRepository : CosmosDbRepository<Form>, IFormRepository
    {
        public FormRepository(IApplicationDbContext dbContext)
            : this(dbContext, "Forms", "/Type")
        { }
        protected FormRepository(IApplicationDbContext dbContext, string containerId, string partitionKeyPath)
            : base(dbContext, containerId, partitionKeyPath)
        { }
    }
}
