using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FileHandling
{
    internal class JSONSerialization
    {
        public static void Run()
        {
            //Program.jasonSerialization();
            //Program.jasonSerializationModify();
            //Program.NestedJson();
            Program.JsonValidation();
        }
    }
    class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string department { get; set; }

    }
    class Department
    {
        public int deptId { get; set; }
        public string deptName { get; set; }
        public List<Employee> employees { get; set; }
    }
    class Program
    {
        public static void jasonSerialization()
        {
            Employee emp = new Employee()
            {
                id = 101,
                name = "John Doe",
                department = "HR"
            };
            // Serialize the Employee object to JSON
            string jsonString = JsonSerializer.Serialize(emp);
            File.WriteAllText("employee.json", jsonString);
            Console.WriteLine("Employee object serialized to employee.json");
            // Deserialize the JSON back to an Employee object
            string readJsonString = File.ReadAllText("employee.json");
            Employee deserializedEmp = JsonSerializer.Deserialize<Employee>(readJsonString);
            Console.WriteLine("Employee object deserialized from employee.json");
            Console.WriteLine($"ID: {deserializedEmp.id}, Name: {deserializedEmp.name}, Department: {deserializedEmp.department}");
        }
        public static void jasonSerializationModify()
        {
            string filePath = "employee.json";
            try
            {
                // Read the existing JSON file
                string jsonString = File.ReadAllText(filePath);
                // Deserialize to Employee object
                Employee emp = JsonSerializer.Deserialize<Employee>(jsonString);
                Console.WriteLine("Current Employee Details:");
                Console.WriteLine($"ID: {emp.id}, Name: {emp.name}, Department: {emp.department}");
                // Modify the Employee object
                emp.department = "Finance"; // Change department
                // Serialize back to JSON
                string updatedJsonString = JsonSerializer.Serialize(emp);
                File.WriteAllText(filePath, updatedJsonString);
                Console.WriteLine("Employee object updated and saved to employee.json");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public static void NestedJson()
        {
            Department dept = new Department()
            {
                deptId = 1,
                deptName = "IT",
                employees = new List<Employee>()
                {
                    new Employee() { id = 201, name = "Alice", department = "IT" },
                    new Employee() { id = 202, name = "Bob", department = "IT" }
                }
            };
            string jsonString = JsonSerializer.Serialize(dept);
            File.WriteAllText("department.json", jsonString);
            Console.WriteLine("Department object serialized to department.json");
            string readJsonString = File.ReadAllText("department.json");
            Department deserializedDept = JsonSerializer.Deserialize<Department>(readJsonString);
            Console.WriteLine("Department object deserialized from department.json");
            Console.WriteLine($"Dept ID: {deserializedDept.deptId}, Dept Name: {deserializedDept.deptName}");
            foreach (var emp in deserializedDept.employees)
            {
                Console.WriteLine($"Employee ID: {emp.id}, Name: {emp.name}, Department: {emp.department}");
            }
        }
        public static void JsonValidation()
        {
            string filePath = "employee.json";

            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("employees.json file does not exist.");
                }

                string jsonData = File.ReadAllText(filePath);
                List<Employee> employees = JsonSerializer.Deserialize<List<Employee>>(jsonData);

                Console.WriteLine("JSON file is valid. Displaying data:\n");
                foreach (var emp in employees)
                {
                    Console.WriteLine($"ID: {emp.id}, Name: {emp.name}, Department: {emp.department}");
                }
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON format error: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
