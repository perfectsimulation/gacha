using System;
using System.Collections.Generic;

static class ModelLocator
{
    static Dictionary<Type, object> m_instances;
    public static object GetModelInstance<T>()
    {
        // Lazy initializer
        if (m_instances == null)
        {
            m_instances = new Dictionary<Type, object>();
        }

        Type forType = typeof(T);
        m_instances.TryGetValue(forType, out object modelInstance);

        if (modelInstance == null)
        {
            modelInstance = Activator.CreateInstance<T>() as object;
            m_instances.Add(forType, modelInstance);
        }

        return modelInstance;
    }

    // Note static classes are never Garbage Collected, so we need an
    // alternative to IDisposable for releasing the references
    public static void Cleanup()
    {
        UnityEngine.Debug.Log("ModelLocator::cleanup");
        foreach (KeyValuePair<Type, object> kvp in m_instances)
        {
            if (kvp.Value is IDisposable)
            {
                (kvp.Value as IDisposable).Dispose();
            }
        }

        m_instances.Clear();
    }
}
