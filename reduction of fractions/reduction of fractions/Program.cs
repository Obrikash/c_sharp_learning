internal class Program
{
    static int GCD(int a, int b)
    {
        a = Math.Abs(a);
        b = Math.Abs(b);
        while (b != 0)
        {
            int remainder = a % b;
            a = b;
            b = remainder;
        }

        return a;
    }
    static void Main(string[] args)
    {
        for (; ; )
        {
            int N = 0;
            int M = 0;

            Console.Write("Введите числитель: ");
            N = Convert.ToInt32(Console.ReadLine());
            while (M <= 0)
            {
                Console.Write("Введите знаменатель: ");
                M = Convert.ToInt32(Console.ReadLine());
                if (M <= 0)
                    Console.WriteLine("Знаменатель должен принадлежать натуральному множеству чисел!");
            }

            int divisor = GCD(M, N);
            if (divisor != 0)
            {
                N /= divisor;
                M /= divisor;
            }
            Console.WriteLine($"Результат: {N} / {M}");
            Console.Write("Напишите Y для продолжения или любую другую букву для выхода из программы: ");
            char exit_var = Console.ReadLine()[0];
            if (exit_var != 'Y')
                break;
        }
    }

}