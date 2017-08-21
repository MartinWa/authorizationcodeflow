using System.Runtime.Serialization;

namespace authorizationcodeflow.Models
{
    public class CallbackModel
    {
        public string Code { get; set; }
        public string State { get; set; }
        [DataMember(Name = "session_state")]
        public string SessionState { get; set; }
    }
}