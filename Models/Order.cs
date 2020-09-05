using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string ChannelName { get; set; }
        public string GlobalChannelName { get; set; }
        [JsonPropertyName("Lines")]
        public ICollection<Product> Products { get; set; }
    }
}