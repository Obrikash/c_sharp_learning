internal class Program
{

    static void Main(string[] args)
    {
        int bacteries = 0;
        Console.Write("Введите количество бактерий: ");
        bacteries = Convert.ToInt32(Console.ReadLine());

        int antibiotics = 0;
        Console.Write("Введите количество антибиотика: ");
        antibiotics = Convert.ToInt32(Console.ReadLine());

        int kills_per_drip = 10;
        int counter = 1;
        while(bacteries > 0 && kills_per_drip > 0)
        {
            bacteries *= 2;
            bacteries -= kills_per_drip-- * antibiotics;
            if (bacteries == 0 || bacteries < 0)
            {
                Console.WriteLine($"После {counter++} часа все бактерии погибли!");
                break;
            }
            Console.WriteLine($"После {counter++} часа бактерий осталось: {bacteries}");
        }
    }
}
