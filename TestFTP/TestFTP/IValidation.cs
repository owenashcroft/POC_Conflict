using System.Collections.Generic;
using System.Linq;

namespace TestFTP
{
    public interface IValidation
    {
        bool IsValid(IEnumerable<string> columns);
    }

    public class FiveColumnOnlyValidation : IValidation
    {
        public bool IsValid(IEnumerable<string> columns)
        {
            return columns.Count() == 5;
        }
    }
}