using System.Collections.Generic;
using Newtonsoft.Json;

namespace Phish.Domain
{
    public class ResponseMessageWithArrayData<T> : ResponseMessageBase<T>
    {
        [JsonProperty("data")]
        public List<T> Data { get; set; }
    }
}