namespace MainTz.Database.Entities
{
    public class RoleEntity : BaseEntity
	{
        public string RoleName { get; set; }
        public ICollection<UserEntity?> User { get; set; }
    }
}