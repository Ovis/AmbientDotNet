using System.Net;
using System.Text;
using System.Text.Json;
using AmbientDotNet.Model;

namespace AmbientDotNet
{
    /// <summary>
    /// AmbientClient
    /// </summary>
    public class AmbientClient
    {
        private readonly HttpClient _clientFactory;

        private readonly string _channelId;
        private readonly string _writeKey;
        private readonly string _readKey;

        private const string Host = "https://ambidata.io";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="writeKey"></param>
        /// <param name="readKey"></param>
        /// <param name="clientFactory"></param>
        public AmbientClient(string channelId, string writeKey, string readKey, HttpClient clientFactory)
        {
            _clientFactory = clientFactory;

            _channelId = channelId;
            _writeKey = writeKey;
            _readKey = readKey;
        }


        /// <summary>
        /// 送信
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, Exception Error)> SendAsync(AmbientRecord record)
        {
            var sendData = new SendData
            {
                WriteKey = _writeKey,
                Records = new[] { record }
            };

            return await SendInternalAsync(sendData);

        }


        /// <summary>
        /// 送信
        /// </summary>
        /// <param name="data1"></param>
        /// <param name="data2"></param>
        /// <param name="data3"></param>
        /// <param name="data4"></param>
        /// <param name="data5"></param>
        /// <param name="data6"></param>
        /// <param name="data7"></param>
        /// <param name="data8"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="comment"></param>
        /// <param name="createdDateTime"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, Exception Error)> SendAsync(
            double? data1 = null,
            double? data2 = null,
            double? data3 = null,
            double? data4 = null,
            double? data5 = null,
            double? data6 = null,
            double? data7 = null,
            double? data8 = null,
            double? latitude = null,
            double? longitude = null,
            string comment = null,
            DateTime? createdDateTime = null)
        {
            var record = new AmbientRecord
            {
                Data1 = data1,
                Data2 = data2,
                Data3 = data3,
                Data4 = data4,
                Data5 = data5,
                Data6 = data6,
                Data7 = data7,
                Data8 = data8,
                Latitude = latitude,
                Longitude = longitude,
                Comment = comment,
                CreatedDateTime = createdDateTime.Value
            };

            var sendData = new SendData
            {
                WriteKey = _writeKey,
                Records = new[] { record }
            };

            return await SendInternalAsync(sendData);
        }

        /// <summary>
        /// 送信内部処理
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        private async Task<(bool IsSuccess, Exception Error)> SendInternalAsync(SendData record)
        {
            {
                var json = JsonSerializer.Serialize(record);

                var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    var url = new StringBuilder();
                    url.Append(Host)
                        .Append("/api")
                        .Append("/v2")
                        .Append("/channels")
                        .Append($"/{_channelId}")
                        .Append("/dataarray");

                    var client = _clientFactory;

                    var response = await client.PostAsync(url.ToString(), jsonContent);

                    return response.StatusCode != HttpStatusCode.OK ? (false, new Exception(response.ReasonPhrase)) : (true, null);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return (false, e);
                }
            }
        }


        /// <summary>
        /// 個数指定取得
        /// </summary>
        /// <param name="getCount"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, AmbientRecord[] Records, Exception Error)> GetAsync(int getCount, int skip = 0)
        {
            try
            {
                var url = new StringBuilder();
                url.Append(Host)
                    .Append("/api")
                    .Append("/v2")
                    .Append("/channels")
                    .Append($"/{_channelId}")
                    .Append("/data?");


                var parameters = new Dictionary<string, string>()
                {
                    { "readKey", _readKey },
                    { "n", getCount.ToString() },
                    { "skip", skip.ToString()}
                };

                var client = _clientFactory;

                var response = await client.GetAsync(url + await new FormUrlEncodedContent(parameters).ReadAsStringAsync());

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return (false, null, new Exception(response.ReasonPhrase));
                }

                var records = JsonSerializer.Deserialize<AmbientRecord[]>(await response.Content.ReadAsStringAsync());

                return (true, records, null);
            }
            catch (Exception e)
            {
                return (false, null, e);
            }
        }


        /// <summary>
        /// 日付指定取得
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, AmbientRecord[] Records, Exception Error)> GetAsync(DateTime date)
        {
            try
            {
                var url = new StringBuilder();
                url.Append(Host)
                    .Append("/api")
                    .Append("/v2")
                    .Append("/channels")
                    .Append($"/{_channelId}")
                    .Append("/data?");

                var parameters = new Dictionary<string, string>()
                {
                    { "readKey", _readKey },
                    { "date", date.ToString("yyyy-MM-dd") }
                };

                var client = _clientFactory;

                var response = await client.GetAsync(url + await new FormUrlEncodedContent(parameters).ReadAsStringAsync());

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return (false, null, new Exception(response.ReasonPhrase));
                }

                var records = JsonSerializer.Deserialize<AmbientRecord[]>(await response.Content.ReadAsStringAsync());

                return (true, records, null);
            }
            catch (Exception e)
            {
                return (false, null, e);
            }
        }


        /// <summary>
        /// 期間指定取得
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, AmbientRecord[] Records, Exception Error)> GetAsync(DateTime start, DateTime end)
        {
            try
            {
                var url = new StringBuilder();
                url.Append(Host)
                    .Append("/api")
                    .Append("/v2")
                    .Append("/channels")
                    .Append($"/{_channelId}")
                    .Append("/data?");

                var parameters = new Dictionary<string, string>()
                {
                    { "readKey", _readKey },
                    { "start", start.ToString("yyyy-MM-dd HH:MM:ss") },
                    { "end", end.ToString("yyyy-MM-dd HH:MM:ss") }
                };

                var client = _clientFactory;

                var response = await client.GetAsync(url + await new FormUrlEncodedContent(parameters).ReadAsStringAsync());

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return (false, null, new Exception(response.ReasonPhrase));
                }

                var records = JsonSerializer.Deserialize<AmbientRecord[]>(await response.Content.ReadAsStringAsync());

                return (true, records, null);
            }
            catch (Exception e)
            {
                return (false, null, e);
            }
        }
    }
}