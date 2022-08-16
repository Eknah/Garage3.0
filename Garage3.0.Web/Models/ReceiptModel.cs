namespace Garage3._0.Web.Models
{
    public class ReceiptModel
    {
        public string? LicenseNumber { get; set; } = "";
        public int PricePerHour { get; set; } = 10;
        public DateTime TimeOfArrival { get; set; }
    }
}
