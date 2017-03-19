using Microsoft.AspNetCore.Hosting;

namespace WebApplication8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseServer(
                    new AwesomeServer(@"C:\sandbox\AwesomeServer\process"))
                .UseStartup<Startup>()

                .Build();

            host.Run();
        }
    }

}
