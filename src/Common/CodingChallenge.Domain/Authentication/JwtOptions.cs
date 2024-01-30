namespace CodingChallenge.Domain.Authentication
{
    public class JwtOptions
    {
        public string Secret { get; set; }

        public string ValidIssuer { get; set; }

        public string ValidAudience { get; set; }
    }
}
