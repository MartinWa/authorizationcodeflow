using IdentityModel.Client;

namespace authorizationcodeflow.Models
{
    public class DiscoveryViewModel
    {
        public string ClientId { get; set; }
        public string Scope { get; set; }
        public string Issuer { get; set; }
        public DiscoveryResponse DiscoveryResponse { get; set; }
    }
}