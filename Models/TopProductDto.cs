namespace Models
{
    public class TopProductDto
    {
        public TopProductDto(Product product, int quantity)
        {
            MerchantProductNo = product.MerchantProductNo;
            Name = product.Name;
            Ean = product.Ean;
            TotalSold = quantity;
        }

        public string MerchantProductNo { get; set; }
        public string Name { get; set; }
        public string Ean { get; set; }
        public int TotalSold { get; set; }
    }
}