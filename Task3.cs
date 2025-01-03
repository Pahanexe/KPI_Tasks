//В цій програмі функція мап припиняє роботу після того як зустрічається із числом нуль.
using System;
using System.Threading;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        int[] numbersWithZero = { 1, 2, 0, 4, 5 }; // Масив із числом 0
        int[] numbersWithoutZero = { 1, 2, 3, 4, 5 }; // Масив без числа 0

        Console.WriteLine("Обробка масиву з числом 0:");
        try
        {
            int[] resultWithZero = await MapAsync(numbersWithZero, async x =>
            {
                await Task.Delay(100); // Імітація асинхронної роботи
                return x * 2;
            });

            Console.WriteLine("Результат:");
            Console.WriteLine(string.Join(", ", resultWithZero));
        }
        catch (OperationCanceledException ex)
        {
            Console.WriteLine($"Операцію скасовано: {ex.Message}");
        }

        Console.WriteLine("\nОбробка масиву без числа 0:");
        try
        {
            int[] resultWithoutZero = await MapAsync(numbersWithoutZero, async x =>
            {
                await Task.Delay(100); // Імітація асинхронної роботи
                return x * 2;
            });

            Console.WriteLine("Результат:");
            Console.WriteLine(string.Join(", ", resultWithoutZero));
        }
        catch (OperationCanceledException ex)
        {
            Console.WriteLine($"Операцію скасовано: {ex.Message}");
        }
        Console.ReadKey();
    }

    public static async Task<int[]> MapAsync(int[] array, Func<int, Task<int>> func)
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        Task<int>[] tasks = new Task<int>[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            int index = i; 
            tasks[i] = Task.Run(async () =>
            {
                token.ThrowIfCancellationRequested(); 
                if (array[index] == 0)
                {
                    cts.Cancel(); 
                    throw new OperationCanceledException("Зустріли число 0. Скасування операції.");
                }
                return await func(array[index]);
            }, token);
        }

        return await Task.WhenAll(tasks);
    }
}

