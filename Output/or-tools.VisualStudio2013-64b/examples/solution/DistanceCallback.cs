﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.OrTools.ConstraintSolver;

namespace TSP
{
    public class DistanceCallback :NodeEvaluator2
    {
        private int[] xs_;
        private int[] ys_;

        private readonly List<double> distance;

        public DistanceCallback(Tuple<double, double>[] locations)
        {
            this.distance = new List<double>();
            foreach (var node in locations)
            {
                var fn = node.Item1;
                foreach (var tn in locations)
                {
                    if (Math.Abs(fn - tn.Item1) == 0)
                    {
                        distance.Add(0);
                    }
                    else
                    {
                        var distBetweenLocation = MathNet.Numerics.Distance.Euclidean(
                            new double[] {node.Item1, node.Item2},
                            new double[] {tn.Item1, tn.Item2});

                        distance.Add(distBetweenLocation);
                    }
                }
            }
        }

        public override long Run(int first_index, int second_index)
        {
            long distance = 0;
            var a = Distance.ToArray();
            Console.WriteLine("running distance call back between {0} and {1}", first_index, second_index);
            for (int i = 0; i < (second_index); i++)
            {
                distance += (long) a[i];
            }
            return distance;
        }

        public List<double> Distance
        {
            get { return distance;}
        } 
    }
}