namespace MainTz.Database.Entities
{
    public class UserCarEntity
    {
        public int CarId { get; set; }
        public CarEntity Car { get; set; }

        public int UserId { get; set; }
        public UserEntity User { get; set; }
    }
}