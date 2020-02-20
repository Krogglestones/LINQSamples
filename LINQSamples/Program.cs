using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Features
{
    class Program
    {
        static void Main(string[] args)
        {

            Func<int, int> square = i => i * i;
            Console.WriteLine(square(9));

            IEnumerable<Employee> developers = new Employee[]
            {
                new Employee {Id = 1, Name = "Craig"},
                new Employee {Id = 2, Name = "Randy"}
            };

            IEnumerable<Employee> sales = new List<Employee>()
            {
                new Employee {Id = 3, Name = "Bret"}
            };

            foreach (var employee in developers.Where(e => e.Name.StartsWith("R")))
            {
                Console.WriteLine(employee.Name);
            }

            foreach (var employee in developers.Where(x => x.Name.Length == 5)
                                               .OrderBy(e => e.Name))
            {
                Console.WriteLine(employee.Name);

            }
        }
    }
}
