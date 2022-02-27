using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AmbientDotNet;
using AmbientDotNet.Model;
using Microsoft.Extensions.Options;

namespace AmbientNetSample
{
    public class Sample
    {
        private readonly HttpClient _client;
        private readonly AppOptions _config;

        public Sample(HttpClient httpClient,IOptions<AppOptions> config)
        {
            _client = httpClient;
            _config = config.Value;
        }


        public async Task RunAsync()
        {
            var ambientLibInstance = 
                new AmbientClient(_config.ChannelId, _config.WriteKey, _config.ReadKey, _client);

            //送信処理(レコード渡し)
            {
                var record = new AmbientRecord
                {
                    Data1 = 1,
                    Data2 = 2,
                    Data3 = 3,
                    Data4 = 4,
                    Data5 = 5,
                    Data6 = 6,
                    Data7 = 7,
                    Data8 = 8,
                    CreatedDateTime = DateTime.UtcNow,
                    Comment = "RecordComment"
                };

                var (isSuccess, error) = await ambientLibInstance.SendAsync(record);

                if (!isSuccess)
                {
                    Console.WriteLine(error);
                }
            }

            //送信処理(値渡し)
            {
                var (isSuccess, error) = await ambientLibInstance.SendAsync(
                    data1: 1.00,
                    data2: 2.00,
                    data3: 3.00,
                    data4: 4.00,
                    data5: 5.00,
                    data6: 6.00,
                    data7: 7.00,
                    data8: 8.00,
                    createdDateTime: DateTime.Now,
                    comment: "comment"
                    );

                if (!isSuccess)
                {
                    Console.WriteLine(error);
                }
            }

            //受信処理(個数指定)
            {
                var (isSuccess, records, error) = await ambientLibInstance.GetAsync(50, 5);

                if (!isSuccess)
                {
                    Console.WriteLine(error);
                }

                foreach (var ambientRecord in records.OrderBy(r => r.CreatedDateTime))
                {
                    Console.WriteLine($"Created:{ambientRecord.CreatedDateTime}, Data1:{ambientRecord.Data1}");
                }

                Console.WriteLine("-------------個数指定ここまで-------------");
            }

            //受信処理(日付指定)
            {
                var date = DateTime.Now;

                var (isSuccess, records, error) = await ambientLibInstance.GetAsync(date);

                if (!isSuccess)
                {
                    Console.WriteLine(error);
                }

                foreach (var ambientRecord in records.OrderBy(r => r.CreatedDateTime))
                {
                    Console.WriteLine($"Created:{ambientRecord.CreatedDateTime}, Data1:{ambientRecord.Data1}");
                }

                Console.WriteLine("-------------日付指定ここまで-------------");
            }

            //受信処理(期間指定)
            {
                var start = DateTime.Now.AddDays(-1);
                var end = DateTime.Now;

                var (isSuccess, records, error) = await ambientLibInstance.GetAsync(start, end);

                if (!isSuccess)
                {
                    Console.WriteLine(error);
                }

                foreach (var ambientRecord in records.OrderBy(r => r.CreatedDateTime))
                {
                    Console.WriteLine($"Created:{ambientRecord.CreatedDateTime}, Data1:{ambientRecord.Data1}");
                }

                Console.WriteLine("-------------期間指定ここまで-------------");
            }
        }

    }
}
