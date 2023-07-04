using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models.Books
{
    public class Books
    {
        public int items { get; set; }
        public List<BookModel> books { get; set; }
    }

    public class BookModel
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("title")] public string? Title { get; set; }

        [JsonProperty("description")] public string? Descriprtion { get; set; }

        [JsonProperty("rate")] public int? Rate { get; set; }

        [JsonProperty("genre")] public string? Genre { get; set; }

        [JsonProperty("cover")] public string? Cover { get; set; }

        [JsonProperty("dateAdded")] public DateTime? DateAdded { get; set; }

        [JsonProperty("publisherId")] public int? PublisherId { get; set; }

        [JsonProperty("authorsIds")] public List<int>? AuthorsIds { get; set; }
    }
}
