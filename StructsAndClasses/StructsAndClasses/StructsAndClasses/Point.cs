using System;

namespace StructsAndClasses
{
    /// <summary>
    /// Represents a 2D point. This single struct intentionally satisfies
    /// several related problems that build on the same type:
    ///   - Problem 1: default + parameterized constructor, ToString override
    ///   - Problem 4: constructor overloading (X only, and X and Y)
    ///   - Problem 5: custom-formatted ToString
    ///   - Problem 6: used as the "value type" example (struct)
    /// </summary>
    public struct Point
    {
        public int X;
        public int Y;

        // ----- Problem 1: default constructor -----
        // Structs always get an implicit parameterless constructor that
        // zero-initializes every field, even if you never write one
        // yourself. Since C# 10, you're also allowed to define your own
        // explicit parameterless constructor, as shown here for clarity.
        public Point()
        {
            X = 0;
            Y = 0;
        }

        // ----- Problem 1 / Problem 4 (part 2): parameterized constructor -----
        // Sets both X and Y to specific values.
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        // ----- Problem 4 (part 1): overloaded parameterized constructor -----
        // Initializes X to a specific value and leaves Y at 0.
        // Having this alongside Point(int, int) above demonstrates
        // constructor overloading: the compiler picks the right one based
        // on how many arguments are supplied.
        public Point(int x)
        {
            X = x;
            Y = 0;
        }

        /// <summary>
        /// The original, simple representation requested by Problem 1:
        /// prints as "(X, Y)". Kept as a separate method so we can show the
        /// "before" state alongside the enhanced ToString() from Problem 5.
        /// </summary>
        public string ToStringSimple() => $"({X}, {Y})";

        // ----- Problem 5: overridden ToString() with custom formatting -----
        // Enhances the plain "(X, Y)" output with labels, satisfying the
        // "include custom formatting" requirement.
        public override string ToString()
        {
            return $"Point(X={X}, Y={Y})";
        }
    }
}
