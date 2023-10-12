using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace InternshipRegistrationAPI.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            InitializeHost();
        }

        private static void InitializeHost() =>
              Host.CreateDefaultBuilder()
                  .ConfigureWebHostDefaults(conf =>
                  {
                      conf.UseStartup<Startup>();
                      conf.UseUrls("http://localhost:8080");
                  }).Build().Run();
    }
}