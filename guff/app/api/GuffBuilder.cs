using guff.app.core.entities;
using System;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace guff
{
    public class GuffBuilder
    {
        private static readonly Channel<LogEvent> _logPipe = Channel.CreateBounded<LogEvent>(new BoundedChannelOptions(250)
        {
            FullMode = BoundedChannelFullMode.Wait,
            AllowSynchronousContinuations = true,
            SingleReader = true,
        });

        public static void Init()
        {
            var jsonOptions = new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = false,
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
            };

            var reader = _logPipe.Reader;
            Task.Run(async () =>
            {
                var dtPattern = "yyyy-MM-ddTHH:mm:ss.fffffffK";
                while (await reader.WaitToReadAsync())
                {
                    while (reader.TryRead(out var item))
                    {
                        try
                        {
                            Console.WriteLine(DateTimeOffset.UtcNow.ToString(dtPattern) + " - " + JsonSerializer.Serialize(item, jsonOptions));
                        } 
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                        
                }

                Console.WriteLine("channel good bye");
                    
            });
        }

        public static IGuffLogger<T> Build<T>() where T : class
        {            
            return new LogInputStream<T>(_logPipe.Writer);
        }
    }
}
