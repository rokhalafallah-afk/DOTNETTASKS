using System;

namespace StructsAndClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            Region1_PointBasics();
            Region2_AccessModifiers();
            Region3_EmployeeEncapsulation();
            Region4_ConstructorOverloading();
            Region5_CustomToStringFormatting();
            Region6_ValueVsReferenceType();

            Console.WriteLine("\nAll demos complete. Press any key to exit.");
            Console.ReadKey();
        }

        #region Region 1 - Point struct: constructors + ToString override
        // Q: Why can't a struct inherit from another struct or class in C#?
        // A: Structs are value types, and the CLR needs to know their exact,
        //    fixed size at compile time so they can be stored inline (on the
        //    stack, inside arrays, inside other structs, etc.) and copied by
        //    value efficiently. Inheritance relies on reference semantics
        //    and dynamic dispatch (a vtable pointer, polymorphic upcasting/
        //    downcasting) which only make sense for objects allocated on the
        //    heap and accessed through references - exactly what classes
        //    provide. Allowing a struct to inherit another struct or class
        //    would break the guarantee of a fixed, predictable layout and
        //    value-copy semantics. Every struct implicitly derives from
        //    System.ValueType (which derives from System.Object), and C#
        //    stops the chain there - structs are sealed with respect to
        //    further inheritance. They CAN, however, implement interfaces,
        //    since interfaces don't require a shared memory layout.
        static void Region1_PointBasics()
        {
            Console.WriteLine("=== Region 1: Point struct - constructors and ToString ===");

            Point defaultPoint = new Point();               // default constructor
            Point specificPoint = new Point(3, 7);           // parameterized constructor

            Console.WriteLine("Default point:  " + defaultPoint.ToStringSimple());
            Console.WriteLine("Specific point: " + specificPoint.ToStringSimple());
            Console.WriteLine();
        }
        #endregion

        #region Region 2 - Access modifiers (TypeA: F private, G internal, H public)
        // Q: How do access modifiers impact the scope and visibility of a
        //    class member?
        // A: Access modifiers control WHERE in the codebase a member can be
        //    referenced from:
        //      - private:   only from within the declaring class itself.
        //      - internal:  from anywhere within the same
        //                   project/assembly, but not from other projects
        //                   that reference it.
        //      - protected: from the declaring class and any class that
        //                   derives from it (regardless of assembly).
        //      - protected internal: from derived classes OR from anywhere
        //                   in the same assembly (whichever is broader).
        //      - private protected: only from derived classes that are ALSO
        //                   in the same assembly (the narrowest combo).
        //      - public:    from anywhere at all, including other projects.
        //    Choosing the right modifier is central to encapsulation: it
        //    determines your type's public "contract" versus its hidden
        //    implementation details, which in turn affects how safely the
        //    implementation can change later without breaking other code.
        static void Region2_AccessModifiers()
        {
            Console.WriteLine("=== Region 2: Access Modifiers (TypeA) ===");
            AccessDemo.Run();
            Console.WriteLine();
        }
        #endregion

        #region Region 3 - Employee struct: encapsulation
        // Q: Why is encapsulation critical in software design?
        // A: Encapsulation hides an object's internal state behind a
        //    controlled interface (methods/properties), which:
        //      - Protects invariants - e.g. Employee.Salary rejects negative
        //        values, something a public field could never enforce.
        //      - Reduces coupling - other code depends only on the public
        //        contract (GetName/SetName, properties), not on internal
        //        field names or storage details.
        //      - Allows the implementation to change later (e.g. how salary
        //        is stored or validated) without breaking any code that
        //        uses the class/struct.
        //      - Makes bugs easier to track down, since all the logic that
        //        can modify a piece of state lives in one place instead of
        //        being scattered across the whole codebase.
        static void Region3_EmployeeEncapsulation()
        {
            Console.WriteLine("=== Region 3: Employee struct - Encapsulation ===");

            Employee emp = new Employee(101, "Alice Johnson", 75000);
            Console.WriteLine("Initial: " + emp);

            // Access via method (as requested by the problem)
            Console.WriteLine("GetName(): " + emp.GetName());
            emp.SetName("Alice M. Johnson");
            Console.WriteLine("After SetName(): " + emp);

            // Access via property, including validation
            emp.Salary = 80000;
            Console.WriteLine("After raising Salary property: " + emp);

            try
            {
                emp.Salary = -500; // rejected by the property setter
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Blocked invalid change: " + ex.Message);
            }

            Console.WriteLine();
        }
        #endregion

        #region Region 4 - Point struct: constructor overloading
        // Q: What is "constructors in structs"?
        // A: A constructor in a struct is a special method used to
        //    initialize a struct instance's fields when it's created.
        //    Structs always have an implicit parameterless constructor that
        //    zero-initializes every field, even if you never define one
        //    (and since C# 10 you're allowed to write your own explicit
        //    parameterless constructor too). Beyond that, you can define
        //    one or more PARAMETERIZED constructors to initialize fields to
        //    specific values, and - as shown here - you can define several
        //    constructors with different parameter lists (constructor
        //    overloading), letting the compiler pick the right one based on
        //    the arguments supplied at the call site. One important rule:
        //    for a struct's own parameterized constructor, every field must
        //    end up definitely assigned by the time the constructor
        //    finishes (either explicitly, or - since C# 11 - implicitly to
        //    its default value).
        static void Region4_ConstructorOverloading()
        {
            Console.WriteLine("=== Region 4: Point struct - Constructor Overloading ===");

            Point p1 = new Point();       // default constructor -> (0, 0)
            Point p2 = new Point(5);      // overload: X = 5, Y = 0
            Point p3 = new Point(5, 9);   // overload: X = 5, Y = 9

            Console.WriteLine("new Point()    -> " + p1.ToStringSimple());
            Console.WriteLine("new Point(5)   -> " + p2.ToStringSimple());
            Console.WriteLine("new Point(5,9) -> " + p3.ToStringSimple());
            Console.WriteLine();
        }
        #endregion

        #region Region 5 - Point struct: custom-formatted ToString
        // Q: How does overriding methods like ToString() improve code
        //    readability?
        // A: The default ToString() (inherited from System.Object, or from
        //    System.ValueType for structs) just prints the fully-qualified
        //    type name, which is essentially useless for debugging or
        //    logging. Overriding it lets an object describe ITSELF in a
        //    meaningful, human-readable way wherever it's printed - in
        //    Console.WriteLine, string interpolation, log files, or a
        //    debugger's watch window - without every caller having to
        //    manually know and repeat the object's internal field names.
        //    This centralizes the "how do I display this" logic in one
        //    place, keeps output consistent everywhere the type is used,
        //    and makes code that prints objects far easier to read.
        static void Region5_CustomToStringFormatting()
        {
            Console.WriteLine("=== Region 5: Point struct - Custom ToString Formatting ===");

            Point[] points =
            {
                new Point(),
                new Point(4),
                new Point(1, 2),
                new Point(-3, 8)
            };

            Console.WriteLine("Before override, simple format (Problem 1 style):");
            foreach (Point p in points)
            {
                Console.WriteLine("  " + p.ToStringSimple());
            }

            Console.WriteLine("\nAfter override, custom format (Problem 5 style):");
            foreach (Point p in points)
            {
                // This implicitly calls the overridden ToString().
                Console.WriteLine("  " + p);
            }

            Console.WriteLine();
        }
        #endregion

        #region Region 6 - Value type (struct) vs reference type (class) behavior
        // Q: How does memory allocation differ for structs and classes in
        //    C#?
        // A: A struct is a VALUE type: a local struct variable's data lives
        //    directly wherever the variable itself lives - on the stack for
        //    a local variable, or inline inside whatever array/object
        //    contains it. When you pass a struct to a method or assign it
        //    to another variable, the ENTIRE value is copied; the method
        //    gets its own independent copy, so changes inside the method
        //    don't affect the caller's original.
        //    A class is a REFERENCE type: the object's data is allocated on
        //    the managed HEAP, and the variable itself only holds a
        //    reference (pointer) to that heap location. When you pass a
        //    class instance to a method, only the reference is copied - both
        //    the caller's variable and the method's parameter point to the
        //    SAME underlying object, so changes made inside the method to
        //    the object's fields ARE visible to the caller afterward.
        static void Region6_ValueVsReferenceType()
        {
            Console.WriteLine("=== Region 6: Struct (value type) vs Class (reference type) ===");

            Point point = new Point(1, 1);
            EmployeeClass employee = new EmployeeClass(202, "Bob Smith", 60000);

            Console.WriteLine("Before method calls:");
            Console.WriteLine("  point:    " + point);
            Console.WriteLine("  employee: " + employee);

            ModifyPoint(point);
            ModifyEmployee(employee);

            Console.WriteLine("\nAfter method calls:");
            Console.WriteLine("  point:    " + point + "   <-- unchanged (struct was copied)");
            Console.WriteLine("  employee: " + employee + "   <-- changed! (class passed by reference)");

            Console.WriteLine();
        }

        static void ModifyPoint(Point p)
        {
            // Modifies only the LOCAL COPY of the struct that was passed in.
            p.X = 999;
            p.Y = 999;
        }

        static void ModifyEmployee(EmployeeClass e)
        {
            // 'e' is a reference to the SAME object the caller has, so this
            // mutation is visible after the method returns.
            e.Salary = 999999;
        }
        #endregion
    }
}
