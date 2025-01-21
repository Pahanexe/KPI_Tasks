using System;

public class Mapper
{
    public IAsyncResult Map(int[] array, Func<int, int> func, AsyncCallback callback)
    {
        if (array == null) throw new ArgumentNullException(nameof(array));
        if (func == null) throw new ArgumentNullException(nameof(func));

        Func<int[], Func<int, int>, int[]> operation = (arr, func) =>
        {
            int[] result = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                result[i] = func(arr[i]);
            }
            return result;
        };

        return operation.BeginInvoke(array, func, callback, null);
    }
}

public class Task1
{
    public static void Main()
    {
        var mapper = new Mapper();

        int[] array = { 1, 2, 3, 4, 5 };
        mapper.Map(array, x => x * 2, ar =>
        {
            var result = ((Func<int[], Func<int, int>, int[]>)((AsyncResult)ar).AsyncDelegate).EndInvoke(ar);
            Console.WriteLine("Результат: " + string.Join(", ", result));
        });

        Console.WriteLine("Операція запущена.");
    }
}