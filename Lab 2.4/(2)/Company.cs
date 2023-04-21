using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_2._4
{

    class Company
    {
       
        public List<Tariffs> TariffsList()
        {
            List<Tariffs> tariffs = new List<Tariffs>()
            {
               new Tariffs("LIMITLESS", "limitless", 750, 700),
               new Tariffs("COMFORT", "limitless", 300, 350),
               new Tariffs("YOUR BEST", "limitless", 200, 250),
               new Tariffs("YOUR CHOICE", "20", 100, 175),
               new Tariffs("SUPER GB", "limitless", 100, 300)
            };


            return tariffs;
        }
        public List<Clients> ClientsList()
        {
            List<Clients> clients = new List<Clients>()
        {
            new Clients("Viktor", "Dorichenko", "0675553322"),
            new Clients("Taras", "Pirnov", "0674553922"),
            new Clients("Nikolo", "Pirnov", "0674553911"),
            new Clients("Alina", "Kolesnyk", "0684433901"),
            new Clients("Anna", "Lipko", "0687773801"),
            new Clients("Fedor", "Lipko", "0677550806")
        };

            return clients;
        }

    }
    class Tariffs : Company
    {
        protected string TariffName { get; set; }
        protected string MobInternetGB { get; set; }
        protected int MinOfCallsToOtherOper { get; set; }
        protected int Value { get; set; }

        public Tariffs(string name, string mobInternetGB, int minOfCallsToOtherOper, int value)
        {
            TariffName = name;
            MobInternetGB = mobInternetGB;
            MinOfCallsToOtherOper = minOfCallsToOtherOper;
            Value = value;
            Console.WriteLine($"\nTariff {TariffName}:\nThe number of GB of mobile internet: {MobInternetGB}\n" +
                $"The number of minutes to other operators: {MinOfCallsToOtherOper}\nCosts: {Value} UAH/per month");
        }
        public Tariffs()
        {

        }
      
        
        public List<Tariffs> SortedTariffsList(List<Tariffs> tariffs, int minValue, int maxValue, string internetGB)
        {
            var sortedTariffs = tariffs.Where(t => t.Value >= minValue && t.Value <= maxValue && t.MobInternetGB == internetGB );
           
           
            return sortedTariffs.ToList();

            
        }
        public List<Tariffs> SortedTariffsList()
        {

            Tariffs tariffs = new Tariffs();
            var sortedTariffsList = tariffs.TariffsList();

             List<Tariffs> sortedTariffs = tariffs.SortedTariffsList(sortedTariffsList, 100, 350, "20");

             for (int j = 0; j < sortedTariffs.Count; j++)
             {
                 Console.WriteLine($"\nTariff upon request\nTariff {sortedTariffs[j].TariffName}:\nThe number of GB of mobile internet: {sortedTariffs[j].MobInternetGB}\n" +
                                $"The number of minutes to other operators: {sortedTariffs[j].MinOfCallsToOtherOper}\nCosts: {sortedTariffs[j].Value} UAH/per month\n");
             }

             return sortedTariffs;
           

        }


    } 

         class Clients : Company
        {
            protected string ClientName { get; set; }
            protected string SurName { get; set; }
            protected string ClientNimber { get; set; }

            public Clients(string clientName, string surName, string number)
            {
                ClientName = clientName;
                SurName = surName;
                ClientNimber = number;
                Console.WriteLine($"\nClient: {ClientName} {SurName}. Number: {ClientNimber}");
            }
            public Clients()
        {

        }
            public string AmountOfClients(List<Clients> clients)
            {
                int amount = 0;
                for(int i = 0; i < clients.Count; i++)
                {
                    amount++;
                }
                return $"\nAmount of clients {amount}\n";

            }


}
    }
    
