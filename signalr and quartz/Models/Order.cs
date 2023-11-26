namespace signalr_and_quartz.Models
{
    public class Order
    {
        public string? Id { get; set; }
        public string? Adress { get; set; }
        public DateTime ExpectedDate { get; set; }

        public bool IsDeliverd { get; set; }// 1 = Deliverd , 0 = not Deliverd
    }
}
