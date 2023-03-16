using System.Net.Http;
using System.Text.Json;
using WindPowerWebApp.Model;

namespace WindPowerWebApp.Service
{
    public class SystemControlService
    {
        static readonly HttpClient client = new();
        readonly string RpiUrl;

        public SystemControlService(IConfiguration configuration)
        {
            RpiUrl = configuration.GetValue<string>("RpiUrl");
            client.BaseAddress = new Uri(RpiUrl);
        }
        public async Task<RpiStatusModel?> PingAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("");
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                var res = await response.Content.ReadFromJsonAsync<RpiStatusModel>();
                res.Online = true;
                return res;
            }
            catch
            {
                return null;
            }

        }

        public async Task<bool> StartSendingAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("/start_sending");
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> StopSendingAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("/stop_sending");
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> ChangeSendingIntervalAsync(float interval)
        {
            try
            {
                var body = new { interval };
                var jsonBody = JsonSerializer.Serialize(body);

                var content = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/change_interval", content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error sending POST request: {response.StatusCode}");
                }
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
