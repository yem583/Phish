using Newtonsoft.Json;

namespace Phish.Domain
{
    public class ResponseContainerWithArray<T>
    {
        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }

        [JsonProperty("response")]
        public ResponseMessageWithArrayData<T> Response { get; set; }
    }
}