using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Eze.AdminConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));

            if (isService)
            {
                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                var pathToContentRoot = Path.GetDirectoryName(pathToExe);
                Directory.SetCurrentDirectory(pathToContentRoot);
            }

            // var builder = CreateWebHostBuilder(
            //    args.Where(arg => arg != "--console").ToArray());
            // var host = builder.Build();

            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseIISIntegration()
                .UseStartup<Startup>()
                .ConfigureKestrel((context, options) =>
                {
                    // Set properties and call methods on options
                })
                .Build();


            if (isService)
            {
                // To run the app without the CustomWebHostService change the
                // next line to host.RunAsService();
                host.RunAsService();
            }
            else
            {
                host.Run();
            }
        }
    }
}
