using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dot.Net.Core.Common.Utils
{
    public interface IManageApi
    {
        Task<T> GetSynch<T>(string controller, string action = null, Dictionary<string, string> data = null, bool keyValuPairFlag = false);

    }
}
