using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flow.Callbacks
{
    public class NodeTuple
    {
        private double _x;
        private double _y;

        public NodeTuple(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public NodeTuple()
        {
            
        }

        public double X
        {
            get { return _x; }
            set { _x = value; }
        }

        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }
    }
}
