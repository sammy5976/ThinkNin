using System;
using System.Collections.Generic;

namespace TSP
{
    public class RoutingData
    {
        public RoutingData ()
        {
            Records = new List<Record>();
            Locations = new List<Tuple<double, double>>();
            Demand = new List<double>();
        }

        public IList<Record> Records { get; set; }

        public List<Tuple<double, double>> Locations { get; set; }

        public List<double> Demand { get; set; } 

        public int Vehicles { get; set; }

        public int Depot { get; set; }
    }
}
