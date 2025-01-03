using System;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        int[] numbers = { 1, 2, 3, 4, 5 };

        // Використання асинхронної функції Map із паралельними тасками
        int[] doubled = await MapAsync(numbers, async x =>
        {
            await Task.Delay(100); // Імітація асинхронної роботи
            return x * 2;
        });
        Console.WriteLine("Результат після подвоєння:");
        Console.WriteLine(string.Join(", ", doubled));

        int[] squared = await MapAsync(numbers, async x =>
        {
            await Task.Delay(100); // Імітація асинхронної роботи
            return x * x;
        });
        Console.WriteLine("Результат після піднесення до квадрата:");
        Console.WriteLine(string.Join(", ", squared));

        Console.ReadLine();
    }

    public static async Task<int[]> MapAsync(int[] array, Func<int, Task<int>> func)
    {
        // Створюємо масив тасків, де кожен таск обробляє один елемент масиву
        Task<int>[] tasks = new Task<int>[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            tasks[i] = func(array[i]); // Запускаємо таск для кожного елемента
        }

        // Очікуємо завершення всіх тасків
        int[] result = await Task.WhenAll(tasks);
        return result;
    }
}
