using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloSGBotService.Service
{
    public interface IHTTPService
    {
        string ServiceURL { get; set; }
        Task<T> Get<T>(string input);
    }
}
