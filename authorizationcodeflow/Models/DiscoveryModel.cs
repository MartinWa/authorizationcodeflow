using System.ComponentModel.DataAnnotations;

namespace authorizationcodeflow.Models
{
    public class DiscoveryModel
    {
        [Required]
        [DataType(DataType.Url)]
        public string Url { get; set; }
    }
}