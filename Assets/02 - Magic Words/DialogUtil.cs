using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace _02___Magic_Words
{
    public static class DialogUtil
    {
        public static async Task<DialogData> GetDialog(string url)
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(url);

                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<DialogData>(responseBody);

                await LoadImages(data.Avatars.Concat<Image>(data.Emojis));

                return data;
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
        }

        static async Task LoadImages(IEnumerable<Image> images)
        {
            var tasks = new List<Task>();
            foreach (var image in images)
            {
                tasks.Add(LoadSingleImage(image));
            }

            await Task.WhenAll(tasks);
        }

        static async Task LoadSingleImage(Image img)
        {
            var texture = await LoadTextureFromUrl(img.URL);
            img.Texture = texture;
        }

        public static async Task<Texture2D> LoadTextureFromUrl(string url)
        {
            using var request = UnityWebRequestTexture.GetTexture(url);
            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning($"Failed to load texture from URL: {request.error}");
                return null;
            }

            return DownloadHandlerTexture.GetContent(request);
        }
    }
}