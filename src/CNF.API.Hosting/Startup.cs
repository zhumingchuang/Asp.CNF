using System.Configuration;

namespace CNF.API.Hosting
{
    public class Startup<T>
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("aaa");
        }
    }
}
