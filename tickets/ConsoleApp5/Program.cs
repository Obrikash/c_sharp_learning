for(; ; )
{
    Console.WriteLine("Введите номер билета(0 для выхода): ");
    int ticket = Convert.ToInt32(Console.ReadLine());
    if (ticket == 0)
        break;
    int first_sum = 0; int second_sum = 0;
    for(int i = 0; i < 3; ++i)
    {
        first_sum += ticket % 10;
        ticket /= 10;
    }
    for (int i = 0; i < 3; ++i)
    {
        second_sum += ticket % 10;
        ticket /= 10;
    }
    Console.WriteLine(first_sum == second_sum);
}