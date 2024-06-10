using Microsoft.Extensions.Configuration;


namespace WebFotokopi.Persistence
{
    public class Configurations
    {
        public static string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/WebFotokopi.API"));
                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager.GetConnectionString("MsSql");
            }
        }
    }
}
