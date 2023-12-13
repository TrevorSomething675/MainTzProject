namespace MainTz.Application.Models.CarEntities
{
    public class BrandDomainEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<int> ModelsId { get; set; }
    }
}