using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HelloSGBotService.Service
{
    [Serializable]
    public class HTTPService : IHTTPService
    {
        public string ServiceURL { get; set; }

        public async Task<T> Get<T>(string input)
        {
            using (var client = new HttpClient())
            {
                string uri = ServiceURL + input;
                var result = await client.GetAsync(uri);

                if (result.IsSuccessStatusCode)
                {
                    var jsonResponse = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<T>(jsonResponse);
                    return data;
                }
                else {
                    throw new Exception("Request Failed");
                }
            }
        }
    }
}
