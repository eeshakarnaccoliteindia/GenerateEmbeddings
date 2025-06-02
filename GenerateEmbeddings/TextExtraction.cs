using UglyToad.PdfPig;
using System.Text;

namespace GenerateEmbeddings
{
	public class TextExtraction
	{
		// Extract Text from PDF
		public string ExtractTextFromPdf(string filePath)
		{
			var sb = new StringBuilder();

			using (var document = PdfDocument.Open(filePath))
			{
				foreach (var page in document.GetPages())
				{
					sb.AppendLine(page.Text);
				}
			}

			return sb.ToString();
		}
	}
}
