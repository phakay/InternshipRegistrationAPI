using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace InternshipRegistrationAPI.Core.Utils;

public class DataModelHelper
{
    public static List<ValidationResult> DoValidationCheck(object rootObj)
    {
        List<object> visited = new List<object>();
        Queue<object> queue = new Queue<object>();
        List<ValidationResult> validationResults = new List<ValidationResult>();
        
        queue.Enqueue(rootObj);
        
        while (queue.Count > 0)
        {
            var o = queue.Dequeue();
            if (o != null && !visited.Contains(o))
            {
                visited.Add(o);
                
                Validator.TryValidateObject(o, new ValidationContext(o), validationResults,
                    validateAllProperties: true);
                
                var properties = o.GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(p => p.PropertyType != typeof(string));
                foreach (var property in properties)
                {
                    if (property.PropertyType.IsClass && !property.PropertyType.IsArray)
                    {
                        object obj = property.GetValue(o);
                        queue.Enqueue(obj);
                    }
                }
            }
        }

        return validationResults;
    }
}