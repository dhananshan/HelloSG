using HelloSGBotService.Model.LUIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloSGBotService.Service
{
    public interface IAIService
    {
        Task<T> GetIntent<T>(string input);
    }
}
