using Microsoft.AspNetCore.Hosting;
using WebPizzaSite.Interfaces;

namespace WebPizzaSite.Services
{
    public class ImageWorker : IImageWorker
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImageWorker(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public string ImageSave(string url)
        {
            string imageName = Guid.NewGuid().ToString() + ".jpg";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Send a GET request to the image URL
                    HttpResponseMessage response = client.GetAsync(url).Result;

                    // Check if the response status code indicates success (e.g., 200 OK)
                    if (response.IsSuccessStatusCode)
                    {
                        // Define the path to save the image
                        var path = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", imageName);
                        var dir = Path.GetDirectoryName(path);
                        if (!Directory.Exists(dir) && dir != null)
                        {
                            Directory.CreateDirectory(dir);
                        }
                        byte[] imageBytes = response.Content.ReadAsByteArrayAsync().Result;
                        File.WriteAllBytes(path, imageBytes);

                    }
                    else
                    {
                        Console.WriteLine($"Failed to retrieve image. Status code: {response.StatusCode}");
                        return String.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return String.Empty;
            }
            return imageName;
        }
    }
}
