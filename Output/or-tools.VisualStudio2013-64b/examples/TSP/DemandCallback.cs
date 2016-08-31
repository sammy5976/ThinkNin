using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.OrTools.ConstraintSolver;

namespace TSP
{
    public class DemandCallback: NodeEvaluator2
    {
        private int[] xs_;
        private int[] ys_;

        private readonly List<double> _demand;

        public DemandCallback(double[] demand)
        {
            _demand = new List<double>(demand);
        }

        public override long Run(int first_index, int second_index)
        {
            return (long)Demand.Sum();
        }

        public List<double> Demand
        {
            get { return _demand;}
        } 
    }
}
