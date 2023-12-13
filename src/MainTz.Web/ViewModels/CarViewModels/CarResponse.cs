namespace MainTz.Web.ViewModels.CarViewModels
{
    public class CarResponse
    {
		public int? Id { get; set; }
		public string Color { get; set; }
		public bool IsVisible { get; set; }
		public bool IsFavorite { get; set; }
		public string Description { get; set; }

		public ICollection<int> ImagesId { get; set; }

		public ICollection<int> UsersId { get; set; }

		public int ModelId { get; set; }
	}
}
