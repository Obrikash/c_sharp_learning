internal class Program()
{

    static void Main()
    {
        Console.Write("Введите количество модулей: ");
        int n = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введите ширину модуля: ");
        int a = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введите длину модуля: ");
        int b = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введите ширину поля: ");
        int w = Convert.ToInt32(Console.ReadLine());
        Console.Write("Введите длину поля: ");
        int h = Convert.ToInt32(Console.ReadLine());

        int defenseModule = 0; // толщина защитного модуля
        for (; ; )
        {
            int a_defense = a + defenseModule * 2;
            int b_defense = b + defenseModule * 2;

            if ((w / a_defense + h / b_defense) >= n || (w / b_defense + h / a_defense) >= n)
                ++defenseModule;
            else
                break;

        }
        Console.WriteLine($"Ответ d: {defenseModule}");
    }
}
