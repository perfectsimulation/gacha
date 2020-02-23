using System;
using System.Collections.Generic;
using UnityEngine;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
	{
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.items;
	}

    public static List<T> ListFromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        List<T> list = new List<T>();
        for (int i = 0; i < wrapper.items.Length; i++)
        {
            list.Add(wrapper.items[i]);
        }
        return list;
    }

    public static string ToJson<T>(T item)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items = new T[] { item };
        return JsonUtility.ToJson(wrapper, true);
    }

    public static string ToJson<T>(T[] array)
	{
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.items = array;
        return JsonUtility.ToJson(wrapper, true);
	}

    public static string ToJson<T>(List<T> list)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        T[] array = list.ToArray();
        wrapper.items = array;
        return JsonUtility.ToJson(wrapper, true);
    }

    [Serializable]
    private class Wrapper<T>
	{
        public T[] items;
	}
}
