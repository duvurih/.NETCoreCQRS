using Dot.Net.Core.Common.DTO;
using System.Collections.Generic;

namespace Dot.Net.Core.Interfaces.Repository
{
    public interface IPalidromeRepository
    {
        List<PalindromeDTO> GetAll();
        bool Save(PalindromeDTO palindrome);
        bool Update(PalindromeDTO palindrome);
        bool Delete(PalindromeDTO palindrome);
    }
}
