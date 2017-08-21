namespace authorizationcodeflow.Models
{
    public class AuthorizeModel
    {
        public string ClientId { get; set; }
        public string Scope { get; set; }
        public string Issuer { get; set; }
    }
}