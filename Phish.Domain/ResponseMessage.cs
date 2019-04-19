using System.Collections.Generic;
using Newtonsoft.Json;

namespace Phish.Domain
{
    public class ResponseMessage<T>: ResponseMessageBase<T>
    {
        [JsonProperty("data")]
        public Dictionary<int, T> Data { get; set; }
    }
}