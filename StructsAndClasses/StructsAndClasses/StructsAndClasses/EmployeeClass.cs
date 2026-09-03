namespace StructsAndClasses
{
    /// <summary>
    /// Problem 6 asks for "a class Employee" to contrast with a struct
    /// Point. This project already has a *struct* named Employee (from
    /// Problem 3's encapsulation exercise), so to avoid a naming collision
    /// within the same project this reference-type version is called
    /// EmployeeClass instead. It plays the same conceptual role: a class
    /// (reference type) we can compare against Point (value type) when
    /// passing instances to methods.
    /// </summary>
    public class EmployeeClass
    {
        public int EmpId;
        public string Name;
        public double Salary;

        public EmployeeClass(int empId, string name, double salary)
        {
            EmpId = empId;
            Name = name;
            Salary = salary;
        }

        public override string ToString() => $"EmployeeClass[Id={EmpId}, Name={Name}, Salary={Salary:C}]";
    }
}
