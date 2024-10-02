internal class Program
{

    static double Factorial(int n)
    {
        double result = 1;
        for (int i = 2; i <= n; ++i)
        {
            result *= i;
        }
        return result;
    }
    static void Main(string[] args)
    {
        Console.Write("Ввведите число e: ");
        double epsilon = Convert.ToDouble(Console.ReadLine());
        Console.Write("Ввведите число x: ");
        double x = Convert.ToDouble(Console.ReadLine());

        double value_fx = 0;

        double term = x;
        int n = 0;

        while (Math.Abs(term) >= epsilon)
        {
            value_fx += term;
            n++;
            term = (-term * x * x) / ((2 * n) * (2 * n + 1)); // следующий член ряда через предыдущий
        }
        Console.WriteLine($"Значение функции sin({x}): {value_fx}");

        Console.Write($"Введите i-тый член ряда для вычисления: ");
        int i = Convert.ToInt32(Console.ReadLine());
        double i_th_value = Math.Pow(-1, i) * Math.Pow(x, 2 * i + 1) / Factorial(2 * i + 1);
        Console.WriteLine($"{i} член ряда sin: {i_th_value}");
    }
}
