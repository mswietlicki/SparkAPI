namespace SparkAPI.Model
{
    public class AuthResponse
    {
        public string Access_Token { get; set; }
        public string Token_Type { get; set; }
        public long Expires_In { get; set; }
    }
}
