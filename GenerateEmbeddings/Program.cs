// See https://aka.ms/new-console-template for more information
using System.Net;
using GenerateEmbeddings;
using Microsoft.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
	static async Task Main(string[] args)
	{
		// Extracts texts from PDF
		var textExtraction = new TextExtraction();
		string pdfText = textExtraction.ExtractTextFromPdf("C:/Hackathon/Sample-PDF.pdf");

		// Register the Custom Embedding Handler with Semantic Kernel
		var httpClient = new HttpClient();
		var llamaHandler = new LlamaEmbeddingHandler(httpClient);
		var builder = Kernel.CreateBuilder();
		builder.Services.AddSingleton<LlamaEmbeddingHandler>(llamaHandler);
		builder.Build();

		// Generate Embeddings
		var embedding = await llamaHandler.GenerateEmbeddingAsync(pdfText);

		// Store embeddings in Qdrant
		var embeddingStorage = new EmbeddingStorage();
		await embeddingStorage.StoreEmbeddingInQdrantAsync("pdf_collection", id: 1, embedding, new Dictionary<string, object>
		{
			{ "source", "your-document.pdf" }
		});

		Console.WriteLine("PDF embeddings stored in Qdrant successfully!");
	}
}

