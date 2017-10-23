using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace FamilieLaissIdentity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Den Titel für das Konsolenfenster setzen
            Console.Title = "IdentityServer";

            //Den Web-Host erzeugen
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
         WebHost.CreateDefaultBuilder(args)
         .UseStartup<Startup>()
         .Build();
    }
}
