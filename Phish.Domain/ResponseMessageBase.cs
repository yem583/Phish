using Newtonsoft.Json;

namespace Phish.Domain
{
    public class ResponseMessageBase<T>
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}