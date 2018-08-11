using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloSG.Service.Service.ExternalService
{
    public interface IWeatherService
    {
        Task<T> GetContent<T>(string input);
    }
}
