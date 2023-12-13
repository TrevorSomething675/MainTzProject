namespace MainTz.Application.Services
{
    public interface ITokenService
    {
        public string CreateAccessToken(string roles);
        public string CreateRefreshToken(string roles);
    }
}
