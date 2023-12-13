namespace MainTz.Database.Entities
{
    public class ImageEntity : BaseEntity
    {
        public string Name { get; set; }
        public byte[] File { get; set; }
        public int CarId { get; set; }
        public CarEntity Car { get; set; }
    }
}