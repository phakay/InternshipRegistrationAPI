using InternshipRegistrationAPI.Core.Models;

namespace InternshipRegistrationAPI.Data.Contracts;

public interface IFormRepository
{
    Task<Form> GetFormAsync(string id, string partitionKey);
    Task<IEnumerable<Form>> GetFormsAsync();
    Task<Form> UpdateFormAsync(Form form);
}