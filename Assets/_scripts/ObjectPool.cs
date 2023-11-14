using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObjectPool<T> where T : class
{
    private readonly Queue<T> pool = new Queue<T>();
    private readonly T objPrefab; // Add this line to store the prefab

    public ObjectPool(T prefab)
    {
        if (!(prefab is Object))
        {
            throw new ArgumentException("Prefab must be a UnityEngine.Object");
        }

        objPrefab = prefab;
    }

    public T GetObject(Transform parent = null)
    {
        if (pool.Count > 0)
        {
            T obj = pool.Dequeue();
            if (obj is MonoBehaviour)
            {
                (obj as MonoBehaviour).gameObject.SetActive(true);
                (obj as MonoBehaviour).transform.parent = parent;
            }
            return obj;
        }
        else
        {
            T newObj = Object.Instantiate(objPrefab as Object) as T;
            return newObj;
        }
    }

    public void ReturnObject(T obj)
    {
        if (obj is MonoBehaviour)
        {
            (obj as MonoBehaviour).gameObject.SetActive(false);
            (obj as MonoBehaviour).transform.parent = null;
        }
        pool.Enqueue(obj);
    }
}