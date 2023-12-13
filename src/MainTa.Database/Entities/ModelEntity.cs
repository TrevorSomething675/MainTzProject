namespace MainTz.Database.Entities
{
    public class ModelEntity : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<CarEntity> Cars { get; set; }
        public int BrandId { get; set; }
        public BrandEntity Brand { get; set; }
    }
}