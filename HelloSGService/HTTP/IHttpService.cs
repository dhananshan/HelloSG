using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloSGService.HTTP
{
    public interface IHttpService
    {
        string ServiceURL { get; set; }

        Task<T> GetAsync<T>(string input, List<Tuple<string, string>> header = null);
    }
}
