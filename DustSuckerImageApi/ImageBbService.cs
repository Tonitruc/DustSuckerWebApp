using System.Text.Json;

namespace DustSuckerImageApi
{
    public class ImageBbService
    {
        private readonly string DontImportantApiKey = "497e3bb7c14b3b90409c6292522ecb78";

        private readonly HttpClient _httpClient;

        private const string BaseUrl = "https://api.imgbb.com/1/upload";


        public ImageBbService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<string?> Add(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return string.Empty;

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            stream.Position = 0;

            using var form = new MultipartFormDataContent();
            using var content = new StreamContent(stream);

            form.Add(content, "image", DateTime.UtcNow.Ticks.ToString());
            form.Add(new StringContent(DontImportantApiKey), "key");

            var response = await _httpClient.PostAsync(BaseUrl, form);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            using var jsonDoc = JsonDocument.Parse(jsonResponse);
            return jsonDoc.RootElement.GetProperty("data").GetProperty("url").GetString();
        }

        public async Task<FileData?> Get(string url)
        {
            try
            {
                using var client = new HttpClient();
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return null;

                var memoryStream = new MemoryStream();
                await response.Content.CopyToAsync(memoryStream);
                memoryStream.Position = 0; 

                string contentType = response.Content.Headers.ContentType?.ToString() ?? "application/octet-stream";
                string fileName = Path.GetFileName(new Uri(url).LocalPath) ?? "downloaded_file";

                return new FileData(memoryStream, contentType, fileName);
            }
            catch
            {
                return null;
            }
        }
        public class FileData
        {
            public Stream Stream { get; }
            public string ContentType { get; }
            public string FileName { get; }

            public FileData(Stream stream, string contentType, string fileName)
            {
                Stream = stream;
                ContentType = contentType;
                FileName = fileName;
            }
        }
    }
}
