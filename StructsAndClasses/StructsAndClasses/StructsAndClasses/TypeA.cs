namespace StructsAndClasses
{
    /// <summary>
    /// Problem 2: a class with three attributes using three different
    /// access modifiers, so we can demonstrate how each one affects
    /// visibility from other parts of the project.
    /// </summary>
    public class TypeA
    {
        private int F = 1;   // visible ONLY inside TypeA itself
        internal int G = 2;  // visible anywhere in this project/assembly
        public int H = 3;    // visible from anywhere, including other projects

        // Since F is private, it can only be read/written from within
        // TypeA. We expose a public method so other classes can still
        // observe its value without breaking encapsulation.
        public int GetF() => F;
    }
}
