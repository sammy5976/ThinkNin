using System;
using System.Collections.Generic;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;

namespace Flow.Callbacks
{
    public class DistanceCallback
    {
        private readonly List<Tuple<double, double>> _locations;
        public DistanceCallback(List<Tuple<double, double>> locations)
        {
            _locations = locations;
            MatrixContainer = new List<NodeTuple>();

            var locArr = locations.ToArray();
            for (int fromNode = 0; fromNode < locations.Count; fromNode++)
            {
                locArr[fromNode]
                var fnTupe = new NodeTuple(fromNode, 0);

                for (int toNode = 0; toNode < locations.Count; toNode++)
                {
                    if (fromNode == toNode)
                    {
                        double d = Distance.Manhattan(fromNode, toNode);
                    }
                    else
                    {
                        var x1 = locArr[fromNode].Item1;
                        var y1 = locArr[fromNode].Item2;
                        var x2 = locArr[toNode].Item1;
                        var y2 = locArr[toNode].Item2;
                    }
                }
            }
        }

        public long DistanceCalc(double x1, double y1, double x2, double y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        public List<NodeTuple> MatrixContainer { get; set; }

        

    }
}
