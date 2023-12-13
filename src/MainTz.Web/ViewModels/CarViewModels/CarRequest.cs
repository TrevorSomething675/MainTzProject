namespace MainTz.Web.ViewModels.CarViewModels
{
    public class CarRequest
    {
        public int? Id { get; set; }
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Color { get; set; } = null!;
    }
}
