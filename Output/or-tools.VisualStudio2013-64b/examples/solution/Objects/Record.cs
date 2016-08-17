using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flow.Objects
{
    public class Record
    {
        public int Id { get; set; }
        public string UPRN { get; set; }
        public DateTime Date { get; set; }
        public int Easting { get; set; }
        public int Northing { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public int cluster { get; set; }
        public int demand { get; set; }

    }
}
