namespace MainTz.Database.Entities
{
    public class BrandEntity : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ModelEntity> Models { get; set; }
    }
}