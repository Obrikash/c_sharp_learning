internal class Program
{

    static void Main()
    {
        int water_ml = 0;
        Console.Write("Введите количество воды в мл: ");
        water_ml = Convert.ToInt32(Console.ReadLine());
        int milk_ml = 0;
        Console.Write("Введите количество молока в мл: ");
        milk_ml = Convert.ToInt32(Console.ReadLine());
        int count_of_americano = 0;
        int count_of_latte = 0;
        int income = 0;

        while (water_ml >= 300 || (water_ml >= 30 && milk_ml >= 270))
        {
            int choice = 0;
            Console.Write("Выберите напиток (1 - американо, 2 - латте): ");
            choice = Convert.ToInt32(Console.ReadLine());
            if (choice == 1)
            {
                if (water_ml < 300)
                {
                    Console.WriteLine("Мы не можем приготовить этот напиток, не хватает воды.");
                    continue;
                }
                else
                {
                    Console.WriteLine("Ваш напиток готов!");
                    water_ml -= 300;
                    ++count_of_americano;
                    income += 150;
                }
            }
            else if (choice == 2)
            {
                if(water_ml < 30 && milk_ml < 270)
                {
                    Console.WriteLine("Мы не можем приготовить этот напиток, не хватает молока.");
                    continue;
                }
                else
                {
                    Console.WriteLine("Ваш напиток готов!");
                    water_ml -= 30;
                    milk_ml -= 270;
                    ++count_of_latte;
                    income += 170;
                }
            }
        }

        Console.WriteLine("*Отчёт*\nИнгредиентов осталось: ");
        Console.WriteLine($"\tВода: {water_ml} мл");
        Console.WriteLine($"\tМолоко: {milk_ml} мл");
        Console.WriteLine($"Кружок американо приготовлено: {count_of_americano}");
        Console.WriteLine($"Кружок латте приготовлено: {count_of_latte}");
        Console.WriteLine($"Итого: {income} рублей");

    }
}
