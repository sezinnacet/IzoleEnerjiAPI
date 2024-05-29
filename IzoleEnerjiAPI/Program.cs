namespace IzoleEnerjiAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().
              AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
           .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder.UseStartup<Startup>();
               webBuilder.CaptureStartupErrors(true);
               webBuilder.UseSetting("detailedErrors", "true");
           });
    }
}
