using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HelloSGService.HTTP
{
    public class HttpService : IHttpService
    {

        public string ServiceURL { get; set; }

        public async Task<T> GetAsync<T>(string input, List<Tuple<string, string>> header = null)
        {
            using (var client = new HttpClient())
            {
                if (header != null)
                    foreach (var item in header)
                    {
                        client.DefaultRequestHeaders.Add(item.Item1, item.Item2);
                    }

                string uri = ServiceURL + input;
                var result = await client.GetAsync(uri);

                if (result.IsSuccessStatusCode)
                {
                    var jsonResponse = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<T>(jsonResponse);
                    return data;
                }
                else
                {
                    throw new Exception("Request Failed");
                }
            }
        }
    }
}
