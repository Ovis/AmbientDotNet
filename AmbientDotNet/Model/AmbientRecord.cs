using System.Text.Json.Serialization;

namespace AmbientDotNet.Model
{
    /// <summary>
    /// AmbientRecord
    /// </summary>
    public class AmbientRecord
    {
        /// <summary>
        /// 登録日時
        /// </summary>
        [JsonPropertyName("created")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTimeOffset CreatedDateTime { get; set; }


        /// <summary>
        /// データ1
        /// </summary>
        [JsonPropertyName("d1")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double? Data1 { get; set; }

        /// <summary>
        /// データ2
        /// </summary>
        [JsonPropertyName("d2")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double? Data2 { get; set; }

        /// <summary>
        /// データ3
        /// </summary>
        [JsonPropertyName("d3")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double? Data3 { get; set; }

        /// <summary>
        /// データ4
        /// </summary>
        [JsonPropertyName("d4")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double? Data4 { get; set; }

        /// <summary>
        /// データ5
        /// </summary>
        [JsonPropertyName("d5")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double? Data5 { get; set; }

        /// <summary>
        /// データ6
        /// </summary>
        [JsonPropertyName("d6")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double? Data6 { get; set; }

        /// <summary>
        /// データ7
        /// </summary>
        [JsonPropertyName("d7")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double? Data7 { get; set; }

        /// <summary>
        /// データ8
        /// </summary>
        [JsonPropertyName("d8")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double? Data8 { get; set; }

        /// <summary>
        /// 緯度
        /// </summary>
        [JsonPropertyName("lat")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double? Latitude { get; set; }

        /// <summary>
        /// 経度
        /// </summary>
        [JsonPropertyName("lng")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public double? Longitude { get; set; }

        /// <summary>
        /// コメント
        /// </summary>
        [JsonPropertyName("cmnt")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Comment { get; set; } = null;

    }
}
