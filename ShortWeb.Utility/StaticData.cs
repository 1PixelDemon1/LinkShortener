namespace ShortWeb.Utility
{
    public class StaticData
    {
        public static string AuthenticationApiBase { get; set; }

        public const string RoleAdmin = "ADMIN";
        public const string RoleUser = "USER";
        public const string TokenCookie = "JwtToken";

        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
