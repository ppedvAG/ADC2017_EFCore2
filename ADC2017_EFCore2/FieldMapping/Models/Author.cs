using System.Collections.Generic;
using System.Net.Http;

namespace FieldMapping.Models
{
    internal class Author
    {
        private string _validatedUrl;

        public int Id { get; set; }
        public string Name { get; set; }

        public string GetUrl() => _validatedUrl;
        public void SetUrl(string url)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();  // throws an Exception if statusCode is wrong
            }

            _validatedUrl = url;
        }

        public List<Book> Books { get; set; } = new List<Book>();
    }
}
