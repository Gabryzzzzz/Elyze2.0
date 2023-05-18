using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantService
{
    public class TenantService : ITenantService
    {
        private readonly TenantConfiguration _tenantConfiguration;

        public TenantService(TenantConfiguration tenantConfiguration) 
        {
            _tenantConfiguration = tenantConfiguration;
        }

        public TenantResponse GetTenant(string company)
        {
            string apiDomain = _tenantConfiguration.ApiTenantDomain;
            string apiKey = _tenantConfiguration.ApiKey;
            string apiKeyValue = _tenantConfiguration.ApiKeyValue;

            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri($"{apiDomain}{company}");
            request.Method = HttpMethod.Get;
            request.Headers.Add(apiKey, apiKeyValue);

            using (HttpClient httpClient = new HttpClient())
            using (Task<HttpResponseMessage>? response = httpClient.SendAsync(request))
            {
                string result = response.Result.Content.ReadAsStringAsync().Result;
                TenantResponse? apiResponse = JsonConvert.DeserializeObject<TenantResponse>(result);

                if (apiResponse == null)
                {
                    throw new TenantException("Invalid Response Tenant");
                }

                return apiResponse;

            }
        }
    }
}
