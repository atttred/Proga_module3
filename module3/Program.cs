using System.Xml.Serialization;

namespace module3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Employees employees;
            XmlSerializer xmlSerializer = new(typeof(Employees));

            using (FileStream fs = new("employees.xml", FileMode.Open))
            {
                employees = (Employees)xmlSerializer.Deserialize(fs);
            }

            var sortedEmployees = employees.EmployeeList.OrderBy(e => e.HireDate);

            using (StreamWriter sw = new("employees.txt"))
            {
                foreach (var employee in sortedEmployees)
                {
                    sw.WriteLine($"Name: {employee.Name} Position: {employee.Position} HireDate: {employee.HireDate:yyyy-MM-dd}");
                }
            }

            using (FileStream fs = new("sorted_employees.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(fs, new Employees { EmployeeList = sortedEmployees.ToList() });
            }
        }
    }

    [XmlRoot("Employee")]
    public class Employee
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
    }

    [XmlRoot("Employees", Namespace = "")]
    public class Employees
    {
        [XmlElement("Employee")]
        public List<Employee> EmployeeList { get; set; }
    }
}