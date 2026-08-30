# C# Practice Exercises — Solutions & Answers

---

## 1. Divide Two Integers with Exception Handling

```csharp
using System;

class Program
{
    static void Main()
    {
        try
        {
            Console.Write("Enter first integer: ");
            int a = int.Parse(Console.ReadLine());

            Console.Write("Enter second integer: ");
            int b = int.Parse(Console.ReadLine());

            int result = a / b;
            Console.WriteLine($"Result: {result}");
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Error: You cannot divide by zero.");
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: Please enter valid integers.");
        }
        finally
        {
            Console.WriteLine("Operation complete.");
        }
    }
}
```

**Q: What is the purpose of the `finally` block?**
The `finally` block always runs after the `try`/`catch`, whether an exception was thrown or not, and even if the exception wasn't caught. It's used for cleanup code that must execute regardless of outcome — closing files, releasing resources, database connections, or (as here) simply signaling that the operation has finished.

---

## 2. Defensive Input for X and Y (TryParse)

```csharp
using System;

class Program
{
    static void TestDefensiveCode()
    {
        int x = ReadPositiveInt("Enter X (positive integer): ");
        int y;

        do
        {
            y = ReadPositiveInt("Enter Y (must be greater than 1): ");
            if (y <= 1)
                Console.WriteLine("Y must be greater than 1. Try again.");
        } while (y <= 1);

        Console.WriteLine($"X = {x}, Y = {y}");
    }

    static int ReadPositiveInt(string prompt)
    {
        int value;
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (int.TryParse(input, out value) && value > 0)
                return value;

            Console.WriteLine("Invalid input. Please enter a positive integer.");
        }
    }

    static void Main()
    {
        TestDefensiveCode();
    }
}
```

**Q: How does `int.TryParse()` improve program robustness compared to `int.Parse()`?**
`int.Parse()` throws a `FormatException` (or `OverflowException`) if the input isn't a valid integer, which crashes the program unless wrapped in a try/catch. `int.TryParse()` instead returns a `bool` indicating success or failure and never throws for bad input — it's cheaper (no exception overhead) and lets you loop and re-prompt the user cleanly without exception-handling logic cluttering the control flow.

---

## 3. Nullable Integer, Null-Coalescing, HasValue vs Value

```csharp
using System;

class Program
{
    static void Main()
    {
        int? nullableInt = null;

        // Null-coalescing operator: assign default if null
        int result = nullableInt ?? 100;
        Console.WriteLine($"Result using ?? operator: {result}");

        // Demonstrating HasValue
        if (nullableInt.HasValue)
        {
            Console.WriteLine($"Value is: {nullableInt.Value}");
        }
        else
        {
            Console.WriteLine("nullableInt has no value (HasValue = false).");
        }

        // Assign a real value and show Value works safely now
        nullableInt = 42;
        if (nullableInt.HasValue)
        {
            Console.WriteLine($"Now Value is: {nullableInt.Value}");
        }

        // Unsafe access example (commented out — would throw if null):
        // int crash = nullableInt.Value; // only safe because we checked HasValue first
    }
}
```

**Q: What exception occurs when trying to access `.Value` on a null `Nullable<T>`?**
An `InvalidOperationException` is thrown, with a message like "Nullable object must have a value." Always check `.HasValue` (or use `??`, pattern matching, or `?.`) before accessing `.Value` directly.

---

## 4. Array Index Out of Bounds

```csharp
using System;

class Program
{
    static void Main()
    {
        int[] numbers = new int[5] { 10, 20, 30, 40, 50 };

        try
        {
            Console.Write("Enter an index to access (0-4): ");
            int index = int.Parse(Console.ReadLine());

            Console.WriteLine($"Value at index {index}: {numbers[index]}");
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("Error: Index is out of the bounds of the array.");
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: Please enter a valid number.");
        }
    }
}
```

**Q: Why is it necessary to check array bounds before accessing elements?**
Arrays in C# have a fixed length, and accessing an index outside `0` to `Length - 1` throws an `IndexOutOfRangeException`, crashing the program if unhandled. Checking bounds beforehand (e.g. `if (index >= 0 && index < array.Length)`) prevents runtime crashes, avoids relying on exceptions for normal control flow, and produces more predictable, user-friendly behavior.

---

## 5. 3x3 Array — Sum of Rows and Columns

```csharp
using System;

class Program
{
    static void Main()
    {
        int[,] matrix = new int[3, 3];

        // Fill with user input
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write($"Enter value for [{i},{j}]: ");
                matrix[i, j] = int.Parse(Console.ReadLine());
            }
        }

        // Row sums
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            int rowSum = 0;
            for (int j = 0; j < matrix.GetLength(1); j++)
                rowSum += matrix[i, j];
            Console.WriteLine($"Sum of row {i}: {rowSum}");
        }

        // Column sums
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            int colSum = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
                colSum += matrix[i, j];
            Console.WriteLine($"Sum of column {j}: {colSum}");
        }
    }
}
```

**Q: How is the `GetLength(dimension)` method used in multi-dimensional arrays?**
`GetLength(int dimension)` returns the number of elements along a specific dimension of a multi-dimensional array. For a 2D array, `GetLength(0)` gives the number of rows and `GetLength(1)` gives the number of columns. It's the safe, dynamic way to bound loops instead of hardcoding sizes, so the code still works if the array dimensions change.

---

## 6. Jagged Array (3 Rows, Varying Sizes)

```csharp
using System;

class Program
{
    static void Main()
    {
        int[][] jaggedArray = new int[3][];

        for (int i = 0; i < jaggedArray.Length; i++)
        {
            Console.Write($"Enter number of elements for row {i}: ");
            int size = int.Parse(Console.ReadLine());
            jaggedArray[i] = new int[size];

            for (int j = 0; j < size; j++)
            {
                Console.Write($"  Row {i}, element {j}: ");
                jaggedArray[i][j] = int.Parse(Console.ReadLine());
            }
        }

        Console.WriteLine("\nJagged array contents:");
        for (int i = 0; i < jaggedArray.Length; i++)
        {
            Console.Write($"Row {i}: ");
            foreach (int value in jaggedArray[i])
            {
                Console.Write(value + " ");
            }
            Console.WriteLine();
        }
    }
}
```

**Q: How does memory allocation differ between jagged arrays and rectangular arrays?**
A rectangular array (e.g. `int[3,3]`) is a single contiguous block of memory with a fixed size in every dimension. A jagged array (`int[][]`) is an array of references, where each row is its own independently allocated array — rows can have different lengths and live in different memory locations. This makes jagged arrays more flexible (variable row sizes, no wasted space) but adds a level of indirection since each row access follows a reference.

---

## 7. Nullable Reference Types

```csharp
#nullable enable
using System;

class Program
{
    static void Main()
    {
        string? nullableName = null;

        Console.Write("Do you want to provide a name? (y/n): ");
        string answer = Console.ReadLine() ?? "n";

        if (answer.ToLower() == "y")
        {
            Console.Write("Enter your name: ");
            nullableName = Console.ReadLine();
        }

        // Null-forgiveness operator (!): tells the compiler
        // "trust me, this is not null" — use only when you are certain.
        if (nullableName != null)
        {
            string confirmedName = nullableName!;
            Console.WriteLine($"Hello, {confirmedName}!");
        }
        else
        {
            Console.WriteLine("No name was provided.");
        }
    }
}
```

**Q: What is the purpose of nullable reference types in C#?**
Nullable reference types (enabled via `#nullable enable` or project settings) let the compiler track and warn about possible null-reference usage at compile time rather than only at runtime. By distinguishing `string` (expected non-null) from `string?` (may be null), the compiler flags places where a possibly-null value is dereferenced without a check, catching a whole class of `NullReferenceException` bugs before the code ever runs.

---

## 8. Boxing and Unboxing

```csharp
using System;

class Program
{
    static void Main()
    {
        // Boxing: value type -> object
        int originalValue = 123;
        object boxedValue = originalValue;
        Console.WriteLine($"Boxed value: {boxedValue}");

        // Unboxing: object -> value type
        int unboxedValue = (int)boxedValue;
        Console.WriteLine($"Unboxed value: {unboxedValue}");

        // Invalid unboxing example
        try
        {
            double invalidUnbox = (double)boxedValue; // boxedValue is actually an int
            Console.WriteLine(invalidUnbox);
        }
        catch (InvalidCastException)
        {
            Console.WriteLine("Error: Invalid cast — boxedValue is not a double.");
        }
    }
}
```

**Q: What is the performance impact of boxing and unboxing in C#?**
Boxing allocates a new object on the managed heap to wrap the value type and copies the value into it; unboxing copies the value back out and requires a type check/cast. Both operations add CPU overhead (allocation, copying, and eventual garbage collection) compared to working with the value type directly on the stack. In performance-sensitive code or tight loops, frequent boxing/unboxing (e.g. storing `int`s in a non-generic `ArrayList`) can noticeably hurt performance — generics (`List<int>`) avoid this entirely.

---

## 9. SumAndMultiply with `out` Parameters

```csharp
using System;

class Program
{
    static void SumAndMultiply(int a, int b, out int sum, out int product)
    {
        sum = a + b;
        product = a * b;
    }

    static void Main()
    {
        int x = 6, y = 7;
        SumAndMultiply(x, y, out int sum, out int product);

        Console.WriteLine($"Sum: {sum}");
        Console.WriteLine($"Product: {product}");
    }
}
```

**Q: Why must `out` parameters be initialized inside the method?**
The `out` keyword tells the compiler that the caller doesn't need to initialize the argument before the call — the method is responsible for assigning it a definite value before returning. The compiler enforces this: it's a compile-time error if a method with an `out` parameter returns without assigning that parameter on every code path. This guarantees the caller always receives a meaningful, definitely-assigned value back.

---

## 10. Optional Parameter and Named Parameters

```csharp
using System;

class Program
{
    static void PrintRepeated(string text, int times = 5)
    {
        for (int i = 0; i < times; i++)
        {
            Console.WriteLine(text);
        }
    }

    static void Main()
    {
        // Uses the default value (5)
        PrintRepeated("Hello");

        // Overrides the default, using a named parameter
        PrintRepeated(text: "Hi there", times: 3);

        // Named parameters can also be passed in any order
        PrintRepeated(times: 2, text: "Named order doesn't matter");
    }
}
```

**Q: Why must optional parameters always appear at the end of a method's parameter list?**
The compiler matches positional arguments left-to-right by position. If an optional parameter came before a required one, the compiler couldn't tell whether an omitted argument should "skip" the optional parameter or shift the following arguments — the call site would be ambiguous. Requiring all optional parameters to trail the required ones keeps positional calls unambiguous (named arguments can still be given in any order, but the parameter *declaration* itself must keep required-then-optional ordering).

---

## 11. Null-Conditional Operator with a Nullable Array

```csharp
using System;

class Program
{
    static void Main()
    {
        int[]? numbers = null;

        // Null-conditional operator (?.) — safely accesses Length
        // without throwing if numbers is null.
        int? length = numbers?.Length;
        Console.WriteLine(length.HasValue
            ? $"Array length: {length.Value}"
            : "Array is null — no length to report.");

        // Now assign an actual array and try again
        numbers = new int[] { 1, 2, 3, 4 };
        int? length2 = numbers?.Length;
        Console.WriteLine($"Array length: {length2 ?? 0}");

        // Combine ?. with ?[] to safely access an element
        int? firstElement = numbers?[0];
        Console.WriteLine($"First element: {firstElement}");
    }
}
```

**Q: How does the null-conditional (`?.`) operator prevent `NullReferenceException`?**
Normally, calling a member or indexer on a null reference (e.g. `numbers.Length`) throws a `NullReferenceException`. The `?.` operator first checks whether the left-hand operand is null: if it is, the entire expression short-circuits and evaluates to `null` instead of throwing; if it isn't null, the member access proceeds normally. This lets you safely chain member/property access on potentially-null objects without wrapping every access in an explicit `if (x != null)` check.

---

## 12. Switch Expression for Day of the Week

```csharp
using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter a day of the week: ");
        string? day = Console.ReadLine();

        int dayNumber = day?.Trim().ToLower() switch
        {
            "monday"    => 1,
            "tuesday"   => 2,
            "wednesday" => 3,
            "thursday"  => 4,
            "friday"    => 5,
            "saturday"  => 6,
            "sunday"    => 7,
            _           => -1 // unrecognized input
        };

        if (dayNumber == -1)
            Console.WriteLine("Not a recognized day of the week.");
        else
            Console.WriteLine($"{day} is day number {dayNumber} of the week.");
    }
}
```

**Q: When is a switch expression preferred over a traditional `if` statement?**
A switch expression is preferred when you're mapping a single value to one of several discrete outcomes based on equality or pattern matching — it's more concise than a long `if/else if` chain, is itself an *expression* that produces and returns a value directly (rather than requiring separate assignment statements in each branch), and the compiler can warn if a switch over an enum or similar type isn't exhaustive. A traditional `if` statement is better suited to complex boolean conditions, ranges with overlapping logic, or branches with multiple, unrelated conditions.

---

## 13. SumArray using `params`

```csharp
using System;

class Program
{
    static int SumArray(params int[] numbers)
    {
        int sum = 0;
        foreach (int n in numbers)
        {
            sum += n;
        }
        return sum;
    }

    static void Main()
    {
        // Calling with individual values
        int result1 = SumArray(1, 2, 3, 4, 5);
        Console.WriteLine($"Sum of individual values: {result1}");

        // Calling with an actual array
        int[] myArray = { 10, 20, 30 };
        int result2 = SumArray(myArray);
        Console.WriteLine($"Sum of array: {result2}");

        // Calling with no arguments at all (valid with params)
        int result3 = SumArray();
        Console.WriteLine($"Sum of nothing: {result3}");
    }
}
```

**Note:** `params` lets a method accept a variable number of arguments of the same type. Callers can pass a comma-separated list, an actual array, or nothing at all — the compiler packages loose arguments into an array automatically. A method can have only one `params` parameter, and it must be the last parameter in the list.
