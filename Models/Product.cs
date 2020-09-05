using System.Text.Json.Serialization;

namespace Models
{
    public class Product
    {
        [JsonPropertyName("MerchantProductNo")]
        public string Id { get; set; }
        
        [JsonPropertyName("Description")]
        public string Name { get; set; }
        
        [JsonPropertyName("Gtin")]
        public string Ean { get; set; }

        public int Quantity { get; set; }
    }
}