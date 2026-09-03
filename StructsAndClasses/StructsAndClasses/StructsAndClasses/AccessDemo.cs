using System;

namespace StructsAndClasses
{
    /// <summary>
    /// A different class in the same project/assembly as TypeA, used to
    /// show exactly what is and isn't reachable from "outside" TypeA.
    /// </summary>
    public static class AccessDemo
    {
        public static void Run()
        {
            TypeA obj = new TypeA();

            // obj.F;  <-- COMPILE ERROR if uncommented: F is private, so it
            // is only visible from code written inside the TypeA class
            // itself. We can only reach it indirectly through GetF().
            Console.WriteLine("F (private, accessed via GetF()): " + obj.GetF());

            // G is internal: visible anywhere within THIS project/assembly.
            // That's exactly why AccessDemo (a different class, but in the
            // same project) can read it directly.
            Console.WriteLine("G (internal, accessible within the same project): " + obj.G);

            // H is public: accessible from anywhere at all, including
            // classes in other projects that reference this assembly.
            Console.WriteLine("H (public, accessible from anywhere): " + obj.H);
        }
    }
}
