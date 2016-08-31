using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flow.Objects
{
    public class Record
    {
        public double Id { get; set; }
        public string UPRN { get; set; }
        public DateTime Date { get; set; }
        public double Easting { get; set; }
        public double Northing { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public double cluster { get; set; }
        public double demand { get; set; }

    }
}
