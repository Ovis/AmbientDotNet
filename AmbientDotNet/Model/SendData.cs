using System.Text.Json.Serialization;

namespace AmbientDotNet.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class SendData
    {
        /// <summary>
        /// Write Key
        /// </summary>
        [JsonPropertyName("writeKey")]
        public string WriteKey { get; set; }

        /// <summary>
        /// Records
        /// </summary>
        [JsonPropertyName("data")]
        public AmbientRecord[] Records { get; set; }
    }
}
