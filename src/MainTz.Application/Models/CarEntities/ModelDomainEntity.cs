namespace MainTz.Application.Models.CarEntities
{
    public class ModelDomainEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<int> CarsId { get; set; }
        public int BrandId { get; set; }
    }
}