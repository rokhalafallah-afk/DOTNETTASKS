using System;

namespace StructsAndClasses
{
    /// <summary>
    /// Problem 3: demonstrates encapsulation. All fields are private; the
    /// only way to read or modify them from outside is through the
    /// GetName()/SetName() methods (as explicitly requested) and through
    /// properties (the more idiomatic C# alternative), each of which can
    /// enforce rules about how the data is accessed or changed.
    /// </summary>
    public struct Employee
    {
        private int empId;
        private string name;
        private double salary;

        public Employee(int empId, string name, double salary)
        {
            this.empId = empId;
            this.name = name;
            this.salary = salary;
        }

        // ----- Method-based access, as requested by the problem -----
        public string GetName() => name;

        public void SetName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Name cannot be empty.");
            name = newName;
        }

        // ----- Property-based access (idiomatic C# alternative) -----
        public int EmpId
        {
            get => empId;
            private set => empId = value; // read-only from outside the struct
        }

        public double Salary
        {
            get => salary;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Salary cannot be negative.");
                salary = value;
            }
        }

        public override string ToString() => $"Employee[Id={empId}, Name={name}, Salary={salary:C}]";
    }
}
