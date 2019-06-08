using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Blazor.Hosting;

namespace DaedalusBackup.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebAssemblyHostBuilder CreateHostBuilder(string[] args) =>
            BlazorWebAssemblyHost.CreateDefaultBuilder()
                .UseBlazorStartup<Startup>();
    }
}
