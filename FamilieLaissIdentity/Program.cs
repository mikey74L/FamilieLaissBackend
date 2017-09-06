using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace FamilieLaissIdentity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Setzen des Titels für das Konsolen-Fenster
            Console.Title = "IdentityServer";

            //Einen Webhost erzeugen und initialisieren
            var host = new WebHostBuilder()
                .UseKestrel() //Kestrel verwenden
                .UseUrls("http://localhost:5000") //Zu verwendende URL und Port
                .UseContentRoot(Directory.GetCurrentDirectory()) //Das Root-Verzeichnis für den Content festlegen
                .UseIISIntegration() //IIS-Integration verwenden
                .UseStartup<Startup>() //Zu verwendende Startup-Klasse festlegen
                .Build(); //Einen Web-Host erzeugen

            //Die Web-Applikation starten
            host.Run();
        }
    }
}
