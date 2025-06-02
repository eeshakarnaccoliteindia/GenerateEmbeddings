using System.Net.Http.Json;

namespace GenerateEmbeddings
{
	public class LlamaEmbeddingHandler
	{
		private readonly HttpClient _httpClient;

		public LlamaEmbeddingHandler(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		// Create a Custom Embedding Service using the New Handler Model
		public async Task<ReadOnlyMemory<float>> GenerateEmbeddingAsync(string input, CancellationToken ct = default)
		{
			var response = await _httpClient.PostAsJsonAsync("http://localhost:11434/api/embeddings", new
			{
				model = "llama3",
				prompt = input
			}, ct);
			response.EnsureSuccessStatusCode();

			var result = await response.Content.ReadFromJsonAsync<EmbeddingResponse>(cancellationToken: ct);
			return new ReadOnlyMemory<float>(result?.Embedding?.ToArray());
		}

		private class EmbeddingResponse
		{
			public List<float>? Embedding { get; set; } = new();
		}
	}
}
