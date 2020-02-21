using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    public class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcessFile("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            //query syntax demonstrating a join
            var query = from car in cars
                        join manufacturer in manufacturers on car.Manufacturer equals manufacturer.Name
                        orderby car.Combined descending, car.Name ascending
                        select new
                        {
                            manufacturer.Headquarters,
                            car.Name,
                            car.Combined
                        };

            foreach (var car in query.Take(10))
            {
                Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined} MPG");
            }

            Console.WriteLine("-------------------------------------------");

            // extension method syntax demonstrating a join
            var query3 = cars.Join(manufacturers, c => c.Manufacturer, m => m.Name, (c, m) => new
                         {
                             m.Headquarters,
                             c.Name,
                             c.Combined
                         })
                         .OrderByDescending(c => c.Combined).ThenBy(c => c.Name);

            foreach (var car in query3.Take(10))
            {
                Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            }

            Console.WriteLine("-------------------------------------------");

            var query2 = cars.Where(y => y.Year == 2016)
                             .OrderByDescending(c => c.Combined).Take(20);

            foreach (var car in query2)
            {
                Console.WriteLine($"{car.Manufacturer} {car.Name} {car.Combined}");
            }

        }

        private static List<Car> ProcessFile(string path)
        {
            var query = File.ReadAllLines(path)
                // skip the top line
                .Skip(1)
                //  removes blank bottom line
                .Where(line => line.Length > 1)

                .ToList()
                //  custom extension
                .ToCar();

            return query.ToList();
        }

        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query = File.ReadAllLines(path)
                            .Where(l => l.Length > 1)
                            .Select(l =>
                            {
                                var columns = l.Split(',');
                                return new Manufacturer
                                {
                                    Name = columns[0],
                                    Headquarters = columns[1],
                                    Year = int.Parse(columns[2])
                                };
                            });
            return query.ToList();
        }
    }

    public static class CarExtensions
    {
        public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(',');

                yield return new Car
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = (columns[1]),
                    Name = columns[2],
                    Displacement = double.Parse(columns[3]),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])
                };
            }
        }
    }
}
