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
            var individuals = restClient.IndividualsIndex("Smith");

            
            individuals.ForEach(i =>
                {
                    Console.WriteLine(i.FriendlyName);        
                });

            Console.WriteLine("Press enter to quit...");
            Console.ReadLine();

        }
    }
}
