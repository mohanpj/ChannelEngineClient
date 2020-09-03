namespace Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string ChannelName { get; set; }
        public string GlobalChannelName { get; set; }
    }
}