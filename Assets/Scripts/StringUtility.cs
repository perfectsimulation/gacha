using System;
using System.Collections.Generic;
using UnityEngine;

public static class StringUtility
{
    public static T[] FromJsonToArray<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.data;
    }

    public static T FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.data[0];
    }

    public static List<T> ListFromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        List<T> list = new List<T>();
        for (int i = 0; i < wrapper.data.Length; i++)
        {
            list.Add(wrapper.data[i]);
        }

        return list;
    }

    public static string ToJson<T>(T item)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.data = new T[] { item };
        return JsonUtility.ToJson(wrapper, true);
    }

    public static string ToJson<T>(T[] array)
	{
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.data = array;
        return JsonUtility.ToJson(wrapper, true);
	}

    public static string ToJson<T>(List<T> list)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        T[] array = list.ToArray();
        wrapper.data = array;
        return JsonUtility.ToJson(wrapper, true);
    }

    [Serializable]
    private class Wrapper<T>
	{
        public T[] data;
	}
}
