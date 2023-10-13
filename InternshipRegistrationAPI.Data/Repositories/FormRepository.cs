using InternshipRegistrationAPI.Data.Contracts;
using InternshipRegistrationAPI.Core.Models;

namespace InternshipRegistrationAPI.Data.Repositories
{
    public class FormRepository : DataRepository<Form>, IFormRepository
    {
        public FormRepository(IApplicationDbContext dbContext)
            : this(dbContext, "Forms", "/Type")
        { }
        protected FormRepository(IApplicationDbContext dbContext, string containerId, string partitionKeyPath)
            : base(dbContext, containerId, partitionKeyPath)
        { }

        public async Task<Form> GetFormAsync(string id, string partitionKey)
        {
            return await GetAsync(id, partitionKey);

        }

        public async Task<IEnumerable<Form>> GetFormsAsync()
        {
            return await GetAsync();
        }
        public async Task<Form> AddFormAsync(Form form)
        {
            if (string.IsNullOrEmpty(form.Id)) 
            { 
                form.Id = Guid.NewGuid().ToString();

            }
            return await AddAsync(form);
        }
        public async Task<Form> UpdateFormAsync(Form form)
        {
            return await UpdateAsync(form);
        }
    }
}
