using Newtonsoft.Json;

namespace Phish.Domain
{
 
    public class ResponseContainer<T>
    {
        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }

        [JsonProperty("response")]
        public ResponseMessage<T> Response { get; set; }
    }
}