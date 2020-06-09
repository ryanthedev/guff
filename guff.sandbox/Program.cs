using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace guff.sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            GuffBuilder.Init();
            var logger = GuffBuilder.Build<Program>();
            var workers = new List<Task>();
            for(var w = 0; w < 2; w++)
            {
                var ww = w;
                workers.Add(Task.Run(() =>
                {
                    var start = DateTimeOffset.UtcNow;
                    for (var i = 0; i < 10000; i++)
                    {
                        logger.Debug($"worker write", new { ww, i });
                    }
                    logger.Debug("end worker", new { ww, elapsedMs = (DateTimeOffset.UtcNow - start).TotalMilliseconds });
                }));
            }
            Task.WhenAll(workers);
            logger.Debug("end program");
            Console.ReadLine();
        }
    }
}
