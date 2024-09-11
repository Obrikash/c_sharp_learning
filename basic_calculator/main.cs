using System;

public class Calculator
{
    // Method for addition
    public double Add(double a, double b)
    {
        return a + b;
    }

    // Method for subtraction
    public double Subtract(double a, double b)
    {
        return a - b;
    }

    // Method for multiplication
    public double Multiply(double a, double b)
    {
        return a * b;
    }

    // Method for division
    public double Divide(double a, double b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("Division by zero is not allowed.");
        }
        return a / b;
    }
}

public class CalculatorUI
{
    private Calculator calculator;
    private double memoryStorage;

    public CalculatorUI()
    {
        calculator = new Calculator();
        memoryStorage = 0;
    }

    public void Run()
    {
        Console.WriteLine("Simple Calculator with MS (Memory Storage)");
        Console.WriteLine("------------------------------------------");

        try
        {
            Console.Write("Enter the first number (or type 'ms' to use stored memory): ");
            string input1 = Console.ReadLine();
            double num1;

            // Check if the user wants to use memory storage
            if (input1 == "ms")
            {
                num1 = memoryStorage;
                Console.WriteLine($"Using memory storage: {memoryStorage}");
            }
            else
            {
                num1 = Convert.ToDouble(input1);
            }

            Console.Write("Enter an operator (+, -, *, /, ms+, ms-): ");
            string op = Console.ReadLine();

            // If user wants to perform memory operations
            if (op == "ms+")
            {
                memoryStorage += num1;
                Console.WriteLine($"Memory storage is now: {memoryStorage}");
                return;
            }
            else if (op == "ms-")
            {
                memoryStorage -= num1;
                Console.WriteLine($"Memory storage is now: {memoryStorage}");
                return;
            }

            Console.Write("Enter the second number (or type 'ms' to use stored memory): ");
            string input2 = Console.ReadLine();
            double num2;

            // Check if the user wants to use memory storage for the second number
            if (input2 == "ms")
            {
                num2 = memoryStorage;
                Console.WriteLine($"Using memory storage: {memoryStorage}");
            }
            else
            {
                num2 = Convert.ToDouble(input2);
            }

            double result = 0;

            // Perform the calculation based on the operator
            switch (op)
            {
                case "+":
                    result = calculator.Add(num1, num2);
                    break;
                case "-":
                    result = calculator.Subtract(num1, num2);
                    break;
                case "*":
                    result = calculator.Multiply(num1, num2);
                    break;
                case "/":
                    result = calculator.Divide(num1, num2);
                    break;
                default:
                    Console.WriteLine("Error: Invalid operator.");
                    return;
            }

            // Display the result
            Console.WriteLine($"The result of {num1} {op} {num2} is {result}");

            // Option to save the result to memory
            Console.WriteLine("Do you want to store this result in memory? (y/n)");
            char saveToMemory = Console.ReadLine()[0];
            if (saveToMemory == 'y')
            {
                memoryStorage = result;
                Console.WriteLine($"Result stored in memory: {memoryStorage}");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: Invalid input format.");
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }

    }
}

class Program
{
    static void Main()
    {
        CalculatorUI ui = new CalculatorUI();
        while (true)
        {
            ui.Run();
            Console.WriteLine("\nEnter 'y' to continue, otherwise any other character:");
            char op = Console.ReadLine()[0];
            if (op != 'y')
                break;
        }
        Console.WriteLine("Exiting the program...\n");
    }
}
