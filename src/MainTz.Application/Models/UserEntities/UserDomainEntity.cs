namespace MainTz.Application.Models.UserEntities
{
    public class UserDomainEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<int> CarsId { get; set; }
        public ICollection<int> RolesId { get; set; }
        public ICollection<int> NotificationsId { get; set; }
    }
}