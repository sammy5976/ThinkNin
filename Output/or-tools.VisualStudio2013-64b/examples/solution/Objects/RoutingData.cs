using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flow.Objects
{
    public class RoutingData
    {
        public RoutingData ()
        {
            Records = new List<Record>();
            Locations = new List<Tuple<int, int>>();
            Demand = new List<int>();
        }

        public IList<Record> Records { get; set; }

        public List<Tuple<int, int>> Locations { get; set; }

        public List<int> Demand { get; set; } 

        public int Vehicles { get; set; }

        public int Depot { get; set; }
    }
}
