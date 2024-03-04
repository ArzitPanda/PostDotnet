using Newtonsoft.Json.Linq;
using OpenAI_API;
using OpenAI_API.Embedding;
using OpenAI_API.Moderation;
using System.Text;

namespace SocialTree.Utils
{
    public class EmbeddingGenerator
    {
        private static readonly string apiKey = "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";

        public async Task<float[]> Method(string[] text)
        {

            string fullText = string.Join(" ", text);

            string url = "https://api.jina.ai/v1/embeddings";
            HttpClient client = new HttpClient();

         
            client.DefaultRequestHeaders.Add("Authorization", "Bearer jina_d09c8e42374b45ef8daaf0a6c70c7979w9Tx1djPYpiwPbQ0vZvJOxjveV-d");

            var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                input = new[] { fullText },
                model = "jina-embeddings-v2-base-en"
            });

            var response = client.PostAsync(url, new StringContent(jsonPayload, Encoding.UTF8, "application/json")).Result;

            var value = await response.Content.ReadAsStringAsync();

            var responseJson = JObject.Parse(value);

            // Extract the embedding data
            var embeddingData = responseJson["data"];

            // Print the first embedding

            var embeddingArray = embeddingData[0]["embedding"];
            float[] embeddings = new float[embeddingArray.Count()];
            for (int i = 0; i < embeddingArray.Count(); i++)
            {
                embeddings[i] = (float)embeddingArray[i];
            }

            return embeddings;


        }



    }
}
