using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

public class Mapper
{
    public void Map(int[] array, Func<int, int> func, Action<int[]> callback)
    {
        if (array == null) throw new ArgumentNullException(nameof(array));
        if (func == null) throw new ArgumentNullException(nameof(func));
        if (callback == null) throw new ArgumentNullException(nameof(callback));

        // Створення нового потоку для обробки
        new Thread(() =>
        {
            int[] result = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                result[i] = func(array[i]); // Застосування функції
            }

            callback(result); // Виклик колбека з результатом
        }).Start();
    }
}


class Program
{
    static void Main(string[] args)
    {
        var mapper = new Mapper();

        int[] array = { 1, 2, 3, 4, 5 };

        // Функція для обробки кожного елемента
        Func<int, int> func = x => x * 2;

        // Колбек, що викликається після обробки
        Action<int[]> callback = result =>
        {
            Console.WriteLine("Результат: " + string.Join(", ", result));
        };

        // Виклик Map
        mapper.Map(array, func, callback);

        Console.WriteLine("Операція запущена. Очікуйте...");
        Console.ReadKey();
    }
}
