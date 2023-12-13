namespace MainTz.Database.Entities
{
    public class CarEntity : BaseEntity
    {
        public string Color { get; set; }
        public bool IsVisible { get; set; }
        public bool IsFavorite { get; set; }
        public string Description { get; set; }

        public ICollection<ImageEntity> Images { get; set; }
        public ICollection<UserCarEntity> Users { get; set; }
        public int ModelId { get; set; }
        public ModelEntity Model { get; set; }
    }
}