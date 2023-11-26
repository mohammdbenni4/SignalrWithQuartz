namespace signalr_and_quartz.Models
{
    public class OrderDto
    {
        public string? Adress { get; set; }
        public DateTime ExpectedDate { get; set; }

        public bool IsDeliverd { get; set; }// 1 = Deliverd , 0 = not Deliverd
    }
}
