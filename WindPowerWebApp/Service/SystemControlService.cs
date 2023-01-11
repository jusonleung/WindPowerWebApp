namespace WindPowerWebApp.Service
{
    public class SystemControlService
    {
        static HttpClient client = new HttpClient();

        string RpiUrl;

        public SystemControlService(IConfiguration configuration)
        {
            RpiUrl = configuration.GetValue<string>("RpiUrl");
            client.BaseAddress = new Uri(RpiUrl);
        }
        public async Task<bool?> PingAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("");
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                var res = await response.Content.ReadAsStringAsync();
                return bool.Parse(res);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> SystemOnOffAsync(bool _switch)
        {
            try
            {
                HttpResponseMessage response = _switch ? await client.GetAsync("/start") : await client.GetAsync("/stop");
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
    }
}
