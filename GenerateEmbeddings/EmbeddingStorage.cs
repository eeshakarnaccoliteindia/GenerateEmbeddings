using System.Text;
using System.Text.Json;

namespace GenerateEmbeddings
{
	public class EmbeddingStorage
	{
		public async Task StoreEmbeddingInQdrantAsync(string collectionName, long id, ReadOnlyMemory<float> vector, Dictionary<string, object> metadata)
		{
			var client = new HttpClient();
			var url = $"http://localhost:6333/collections/{collectionName}/points";

			var payload = new
			{
				points = new[]
				{
					new
					{
						id = id,
						vector = vector,
						payload = metadata
					}
				}
			};

			var json = JsonSerializer.Serialize(payload);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await client.PutAsync(url, content);
			response.EnsureSuccessStatusCode();
		}
	}
}
