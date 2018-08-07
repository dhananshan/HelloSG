using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloSGService.Service.AI
{
    public interface IAIService
    {
        Task<T> GetIntent<T>(string input);
    }
}
