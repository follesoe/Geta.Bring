using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Geta.Bring.Standard.Test
{
    public static class TestHelper
    {
        public static IConfigurationRoot GetIConfigurationRoot()
        {
            var outputPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .Build();
        }
    }
}
