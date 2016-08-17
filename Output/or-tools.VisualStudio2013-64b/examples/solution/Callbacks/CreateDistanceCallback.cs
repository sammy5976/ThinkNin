using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Flow.Callbacks
{
    public class CreateDistanceCallback
    {
        private readonly List<Tuple<int, int>> _locations;

        public CreateDistanceCallback(List<Tuple<int, int>> locations)
        {
            _locations = locations;
            Matrix = new int[,] {};

            for (int fromNode = 0; fromNode < locations.Count; fromNode++)
            {
                for (int toNode = 0; toNode < locations.Count; toNode++)
                {
                    if (fromNode == toNode)
                    {
                        var a = new int[] {fromNode, toNode};
                        //Matrix
                    }
                }
            }
        }

        public long Distance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        public int[,] Matrix { get; set; }


    }
}
