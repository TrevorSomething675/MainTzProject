namespace MainTz.Application.Models.CarEntities
{
    public class ImageDomainEntity
    {
        public string Name { get; set; }
        public byte[] File { get; set; }
        public int CarId { get; set; }
        public CarDomainEntity Car { get; set; }
    }
}