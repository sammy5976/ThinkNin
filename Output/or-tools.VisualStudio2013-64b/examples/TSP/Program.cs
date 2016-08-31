using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Google.OrTools.ConstraintSolver;
using MathNet.Numerics;

namespace TSP
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new RoutingData();
            var file = File.OpenText("Sample.csv");
            var csv = new CsvReader(file);
            data.Records = csv.GetRecords<Record>().ToList();

            int size = data.Records.Count();

            //get demand
            foreach (var d in data.Records)
            {
                data.Demand.Add(d.demand);
            }

            //Get Location
            foreach (var location in data.Records.Select(rec => new Tuple<double, double>(rec.Northing, rec.Easting)))
            {
                data.Locations.Add(location);
            }

            //set depot
            data.Depot = 0;

            //set vehicles
            data.Vehicles = 5;

            if (data.Records.Count > 1)
            {
                var routing = new RoutingModel(size, data.Vehicles);
                var searchParams = RoutingModel.DefaultSearchParameters();
                searchParams.FirstSolutionStrategy = FirstSolutionStrategy.Types.Value.PATH_CHEAPEST_ARC;

                routing.SetDepot(data.Depot);


                //distance callback
                var distanceBetweenLocs = new DistanceCallback(data.Locations.ToArray());
                var distCallback = distanceBetweenLocs;
                routing.SetArcCostEvaluatorOfAllVehicles(distCallback);

                //demand callback
                var demandAtLocation = new DemandCallback(data.Demand.ToArray());
                var demandCallback = demandAtLocation;

                //capacity demention contrations
                routing.AddDimension(demandCallback, 0, 400, true, "Capacity");

                //solve displays solution if any found
                var assignment = routing.SolveWithParameters(searchParams);
                
                //display solution
                if (!assignment.Empty())
                {
                    for (int i = 0; i < data.Vehicles; i++)
                    {
                        var index = routing.Start(i);
                        var indexNext = assignment.Value(routing.NextVar(index));
                        var route = "";
                        long routeDist = 0;
                        long routeDemand = 0;

                        while (routing.IsEnd(indexNext))
                        {
                            var nodeIndex = routing.IndexToNode(index);
                            var nodeNextIndex = routing.IndexToNode(indexNext);
                            route += $"{nodeIndex} => ";

                            routeDist = distCallback.Run((int) index, (int) indexNext);
                            routeDemand = demandCallback.Run((int) index, (int) indexNext);

                            index = indexNext;
                            indexNext = assignment.Value(routing.NextVar(index));

                        }

                        Console.WriteLine("Route for Vehicle {0} ", i);
                        Console.WriteLine(route);
                        Console.WriteLine("Distance of route {0} ", i);
                        Console.WriteLine(routeDist);
                        Console.WriteLine("Demand met by Vehicle {0} ", i);
                        Console.WriteLine(routeDemand);
                        Console.WriteLine("----------------------------------------");

                    }
                }

                else
                {
                    Console.Write("No Solution Found");
                }

            }

            Console.ReadKey();
        }


    }

    
}
