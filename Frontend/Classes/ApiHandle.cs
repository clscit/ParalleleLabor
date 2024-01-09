using Frontend.Models;
using System.Text;

namespace Frontend.Classes
{
    public class ApiHandle
    {
        //public const string Url = "http://backend_container:80";
        public const string Url = "https://localhost:7146";

        public async Task<List<Entry>> GetAllEntries()
        {
            try
            {
                using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) })
                {
                    var response = await client.GetAsync($"{Url}/DB");
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var entryModel = Newtonsoft.Json.JsonConvert.DeserializeObject<Entry[]>(json);

                    return entryModel?.ToList() ?? new List<Entry>();
                }
            }
            catch (Exception e)
            {
                return new List<Entry>();
            }
        }

        public async Task<Entry> GetEntryById(int id)
        {
            try
            {
                using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) })
                {
                    var response = await client.GetAsync($"{Url}/DB/{id}");
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var entryModel = Newtonsoft.Json.JsonConvert.DeserializeObject<Entry>(json);

                    return entryModel;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> CreateEntry(Entry model)
        {
            try
            {
                using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) })
                {
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"{Url}/DB", content);
                    response.EnsureSuccessStatusCode();

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> UpdateEntry(int id, Entry model)
        {
            try
            {
                using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) })
                {
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync($"{Url}/DB/{id}", content);
                    response.EnsureSuccessStatusCode();

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteEntry(int id)
        {
            try
            {
                using (var client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) })
                {
                    var response = await client.DeleteAsync($"{Url}/DB/{id}");
                    response.EnsureSuccessStatusCode();

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

}