internal class Program
{

    static void Main(string[] args)
    {
        Console.WriteLine("Я могу угадать ваше число меньше чем за 7 попыток!");
        Console.WriteLine("Загадайте число от 0 до 63, а я попробую его угадать!");
        int min = 0; int max = 63; int mid;
        bool bigger = false;
        for(int i = 0; i < 7; ++i)
        {
            mid = (min + max) / 2;

            Console.Write($"#{i+1} Ваше число больше чем {mid}? 1 - да, 0 - нет: ");
            bigger = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));

            if (bigger)
                min = mid + 1;
            else
                max = mid;

            if (min == max)
            {
                Console.WriteLine($"Вы загадали {min}");
                break;
            }
        }
    }
}