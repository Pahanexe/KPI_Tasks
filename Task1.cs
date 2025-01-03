using System;

public class Task1
{
    public static void Main(string[] args)
    {
        int[] numbers = { 1, 2, 3, 4, 5 };

        Map(numbers, x => x * 2, result =>
        {
            Console.WriteLine("Результат після подвоєння:");
            Console.WriteLine(string.Join(", ", result));
        });

        Map(numbers, x => x * x, result =>
        {
            Console.WriteLine("Результат після піднесення до квадрата:");
            Console.WriteLine(string.Join(", ", result));
        });

        Console.ReadKey();
    }

    public static void Map(int[] array, Func<int, int> func, Action<int[]> callback)
    {
        int[] result = new int[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = func(array[i]);
        }

        // Викликаємо колбек із результатом
        callback(result);
    }
}
