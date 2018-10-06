using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Plugin.Connectivity;

namespace ServeAdmin
{
    public class CloudDataStore : IDataStore<Item>
    {
        public static bool error = true;

        public class JSONItems
        {
            public IList<Item> errors { get; set; }
            public String result { get; set; }
            public bool error { get; set; }
        }
        public class JSONNotify {
            public string pass;
            public string phone;
            public bool important;
            public string title;
            public string desc;
        }

        static HttpClient client;
        IEnumerable<Item> items;

        public CloudDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.BackendUrl}/");

            items = new List<Item>();
        }

        public static void Refresh() {
            if (client != null) {
                client.BaseAddress = new Uri($"{App.BackendUrl}/");
            }
        }

        public static void TurnServerOn() {
            if (client != null) {
                client.GetAsync($"php-shit/run_server.php");
            }
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && CrossConnectivity.Current.IsConnected)
            {
                await Task.Run(async () =>
                {
                    var json = await client.GetStringAsync($"server/"+ (error ? "errorlog" : "supportlog") +"/"+ Passwords.user+ "/"+Passwords.password);
                    var itemsRaw = JsonConvert.DeserializeObject<JSONItems>(json);
                    if (!itemsRaw.error)
                    {
                        items = itemsRaw.errors;
                    }
                    else
                    {
                        Item item = new Item();
                        item.Id = 0;
                        item.Text = "Error acquired!";
                        item.Description = itemsRaw.result;
                        items = new Item[] { item };
                    }
                });
            }

            return items;
        }


        public async Task<bool> AddItemAsync(Item item)
        {
            if (item == null || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync($"api/item", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            if (item == null || item.Id == 0 || !CrossConnectivity.Current.IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/item/{item.Id}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            if (id == 0 || !CrossConnectivity.Current.IsConnected)
                return false;

            var response = await client.GetAsync($"server/"+(error ? "errorlog" : "supportlog")+"/" + Passwords.user + "/" + Passwords.password+"/"+id);

            return response.IsSuccessStatusCode;
        }

        public async Task<string> SendNotification(string title, string description, bool important)
        {
            if (title.Equals("") || description.Equals("") || !CrossConnectivity.Current.IsConnected)
                return null;

			var content = new StringContent(JsonConvert.SerializeObject(new JSONNotify
			{
				title = title,
				desc = description,
				important = important,
                pass = Passwords.password,
                phone = Passwords.user
			}), Encoding.UTF8, "text/json");

            var response = await client.PostAsync($"server/sendnotifications", content);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
