// Copyright 2010-2014 Google
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Flow.Objects;
using Google.OrTools.ConstraintSolver;

class Tsp
{
    class RandomManhattan : NodeEvaluator2
    {
        public RandomManhattan(int size, int seed)
        {
            this.xs_ = new int[size];
            this.ys_ = new int[size];
            Random generator = new Random(seed);
            for (int i = 0; i < size; ++i)
            {
                xs_[i] = generator.Next(1000);
                ys_[i] = generator.Next(1000);
            }
        }

        public override long Run(int first_index, int second_index)
        {
            return Math.Abs(xs_[first_index] - xs_[second_index]) +
                   Math.Abs(ys_[first_index] - ys_[second_index]);
        }

        private int[] xs_;
        private int[] ys_;
    };

    class ConstantCallback : NodeEvaluator2
    {
        public override long Run(int first_index, int second_index)
        {
            return 1;
        }
    };

    static void Solve(int size, int forbidden, int seed, RoutingData data )
    {
        if (data.Records.Count < 1)
        {
            Console.WriteLine("No Solution found: Locations entered ");
            return;
        }

        RoutingModel routing = new RoutingModel(size, data.Vehicles);
        routing.SetDepot(data.Depot);

        // Solve, returns a solution if any (owned by RoutingModel).
        RoutingSearchParameters search_parameters =
            RoutingModel.DefaultSearchParameters();
        // Setting first solution heuristic (cheapest addition).
        search_parameters.FirstSolutionStrategy =
            FirstSolutionStrategy.Types.Value.PATH_CHEAPEST_ARC;

        // Setting the cost function.
        // Put a permanent callback to the distance accessor here. The callback
        // has the following signature: ResultCallback2<int64, int64, int64>.
        // The two arguments are the from and to node inidices.
        RandomManhattan distances = new RandomManhattan(size, seed);
        routing.SetCost(distances);

        //Create Distance callbacks

        // Add dummy dimension to test API.
        routing.AddDimension(new ConstantCallback(),
            size + 1,
            size + 1,
            true,
            "dummy");

        

        Assignment solution = routing.SolveWithParameters(search_parameters);
        if (solution != null)
        {
            // Solution cost.
            Console.WriteLine("Cost = {0}", solution.ObjectiveValue());
            // Inspect solution.
            // Only one route here; otherwise iterate from 0 to routing.vehicles() - 1
            for (int route_number = 0; route_number < data.Vehicles; route_number++)
            {
                for (long node = routing.Start(route_number);
                    !routing.IsEnd(node);
                    node = solution.Value(routing.NextVar(node)))
                {
                    Console.Write("{0} -> ", node);
                }
                Console.WriteLine(0);
            }
        }
    }

    public static void Main(String[] args)
    {
        var data = new RoutingData();
        var file = File.OpenText("Sample.csv");
        var csv = new CsvReader(file);
        data.Records = csv.GetRecords<Record>().ToList();

        int size = data.Records.Count();

        //locations 
        foreach (var location in data.Records.Select(rec => new Tuple<int, int>(rec.Northing, rec.Easting)))
        {
            data.Locations.Add(location);
        }

        //demand
        foreach (var demand in data.Records)
        {
            data.Demand.Add(demand.demand);
        }
        //Depot
        data.Depot = 0;

        //Vehicles
        data.Vehicles = 5;

        int forbidden = 0;
       
        int seed = 0;
       

        Solve(size, forbidden, seed, data);
    }
}