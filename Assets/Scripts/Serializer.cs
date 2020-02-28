using System.Collections.Generic;

public static class Serializer
{
    public static T[] ListToArray<T>(List<T> list)
    {
        int count = list.Count;
        T[] items = new T[count];
        int index = 0;
        foreach (T item in items)
        {
            items[index] = item;
            index++;
        }

        return items;
    }

    public static List<T> ListFromArray<T>(T[] array)
    {
        List<T> list = new List<T>();
        foreach (T item in array)
        {
            list.Add(item);
        }

        return list;
    }
}
