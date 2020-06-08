using System;

namespace guff.sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            GuffBuilder.Init();
            var logger = GuffBuilder.Build<Program>();
            var start = DateTimeOffset.UtcNow;
            for(var i = 0; i < 20000; i++)
            {
                logger.Debug(new { test = "testing", i });
            }
            logger.Debug(new { elapsed = DateTimeOffset.UtcNow - start });
            Console.ReadLine();
        }
    }
}
