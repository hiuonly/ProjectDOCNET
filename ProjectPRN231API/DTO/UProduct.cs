namespace ProjectPRN231API.DTO
{
    public class UProduct
    {
        public int productId { get; set; }
        public string pImg { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public int brandId { get; set; }
        public float price { get; set; }
        public int quantity { get; set; }
    }
}
