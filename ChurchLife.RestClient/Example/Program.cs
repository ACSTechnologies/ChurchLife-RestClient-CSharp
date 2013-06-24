using System;
using System.Configuration;
using AcsTech.ChurchLife.RestClient;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var siteNumber = int.Parse(ConfigurationManager.AppSettings["SITE_NUMBER"]);
            var username = ConfigurationManager.AppSettings["USERNAME"];
            var password = ConfigurationManager.AppSettings["PASSWORD"];

            var restClient = new ChurchLifeRestClient(siteNumber, username, password);
            Console.WriteLine("Individuals:");
            var individuals = restClient.IndividualsIndex("Smith");

            
            individuals.Page.ForEach(i =>
                {
                    Console.WriteLine(i.FirstName +" " + i.LastName);        
                });
            Console.WriteLine("\nEvents:");
            var events = restClient.EventsIndex(DateTime.Today, DateTime.Today.AddDays(14));
            events.Page.ForEach(e =>
                {
                    Console.WriteLine("{0}: {1} - {2}", e.EventName, e.StartDate.ToString("g"), e.StopDate.ToString("g"));
                });
            Console.WriteLine("Press enter to quit...");
            Console.ReadLine();

        }
    }
}
