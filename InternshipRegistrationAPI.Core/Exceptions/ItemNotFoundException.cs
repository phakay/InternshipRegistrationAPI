using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipRegistrationAPI.Core.Exceptions
{
    public class ItemNotFoundException : ApplicationException
    {
        public ItemNotFoundException(string message) : base(message) 
        {
                
        }
        public ItemNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
                
        }
    }
}
