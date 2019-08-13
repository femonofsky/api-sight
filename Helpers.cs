using System;
using System.Collections.Generic;

namespace Advantage.API
{
    public class Helpers
    {
        private static Random _rand  = new Random();

        private static string GetRandom(IList<string> items)
        {
             return items[_rand.Next(items.Count)];
        }
        internal static string MakeUniqueCustomerName(List<string> names)
        {
            var maxprefix = bizPrefix.Count * bizSuffix.Count;
            if(names.Count >= maxprefix)
            {
                throw new System.InvalidOperationException("Maximum number of unique names exceed"); 
            }
            var prefix = GetRandom(bizPrefix);
            var suffix = GetRandom(bizSuffix);

            var bizName = prefix + suffix;
            if(names.Contains(bizName)){
                MakeUniqueCustomerName(names);
            }
            return bizName;
        }

        internal static string MakeCustomerEmail(string customerName)
        {
            return $"contact@{customerName.ToLower()}.com";
        }

        internal static string GetRandomState()
        {
            return GetRandom(states);
        }

        internal static decimal GetRandomOrderTotal()
        {

            return _rand.Next(100, 500);
        }

        internal static DateTime getRandomOrderPlaced()
        {
            var endDate = DateTime.Now;
            var startDate = endDate.AddDays(-100);

            TimeSpan possibleSpan = endDate - startDate;
            TimeSpan newSpan = new TimeSpan(0 , _rand.Next(0, (int)possibleSpan.TotalMinutes), 0);
            return startDate + newSpan;
        }

        internal static DateTime getRandomOrderCompleted(DateTime orderPlaced)
        {
            var now = DateTime.Now;
            var minLeadTime = TimeSpan.FromDays(7);
            var timePassed = now - orderPlaced;

            if(timePassed < minLeadTime){
                return now;
            }
            return orderPlaced.AddDays(_rand.Next(7,14));

        }

        private static readonly List<string> states = new List<string>()
        {
            "Lagos",
            "Ogun",
            "Abuja",
            "Oyo",
            "Jos",
            "Abia",
            "Kaduna",
            "Calabar",
            "Paris",
            "New York"
        };

        private static readonly List<string> bizPrefix = new List<string>()
        {
            "ABZ",
            "XYZ",
            "Sales",
            "Enterprise",
            "MainSt",
            "Quick",
            "Read",
            "Budget",
            "Peak",
            "Magic",
            "Family",
            "Comfort" 
        };

        private static readonly List<string> bizSuffix = new List<string>()
        {
            "Corporations",
            "Co",
            "Logistics",
            "Transit",
            "Bankery",
            "Goods",
            "Foods",
            "Cleaners",
            "Hotels",
            "Planner",
            "Funmi",
            "Abiodun" 
        };
    }
}