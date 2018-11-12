using Dot.Net.Core.Common.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Dot.Net.Core.Common.Utils
{
    public class ServiceManager : IManageApi
    {
        IOptions<AppSettingsConfig> _appSettings;

        public ServiceManager(IOptions<AppSettingsConfig> appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<T> GetSynch<T>(string controller, string action = null, Dictionary<string, string> data = null, bool keyValuPairFlag = false)
        {
            string apiParameters = controller + (!string.IsNullOrEmpty(action) ? "/" + action : "");
            int i = 0;
            if (data != null)
            {
                foreach (KeyValuePair<string, string> keyValPair in data)
                {
                    if (keyValuPairFlag == false)
                    {
                        apiParameters = apiParameters + "/" + keyValPair.Value;
                    }
                    else
                    {
                        apiParameters = apiParameters + (i == 0 ? "" : "&") + keyValPair.Key + "=" + keyValPair.Value;
                        i++;
                    }

                }
            }
            HttpClientHandler handler = new HttpClientHandler()
            {
                UseDefaultCredentials = true
            };
            using (HttpClient httpClient = new HttpClient(handler))
            {
                string serviceUrl = _appSettings.Value.ServiceURL + apiParameters;
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync(serviceUrl);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception(response.ReasonPhrase, new Exception(response.Content.ReadAsStringAsync().Result));
                }

                //deserialise to the requested type
                return Deserialize<T>(response);
            }
        }


        /// <summary>
        /// method to deserialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        private T Deserialize<T>(HttpResponseMessage response)
        {
            var deserialized = JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result, SerializerSettings);
            return deserialized;
        }

        private static JsonSerializerSettings SerializerSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    TypeNameHandling = TypeNameHandling.Objects
                };
            }
        }
    }
}
