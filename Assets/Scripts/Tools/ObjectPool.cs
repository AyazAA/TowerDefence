using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private readonly T _objectToPool;
    private int _countToPool;
    private readonly List<T> _poolObjects;
    private readonly Transform _parentObject;

    public ObjectPool(T objectToPool, int count = 5)
    {
        _objectToPool = objectToPool;
        _countToPool = count;
        
        _parentObject = new GameObject().transform;
        _parentObject.name = $"Pool of {typeof(T).ToString()}"; 
        _poolObjects = new List<T>();
        for (int i = 0; i < _countToPool; i++)
        {
            CreateNewObject();
        }
    }

    public T GetPooledObject()
    {
        for (int i = 0; i < _poolObjects.Count; i++)
        {
            if (!_poolObjects[i].gameObject.activeInHierarchy)
            {
                return _poolObjects[i];
            }
        }
        return CreateNewObject();
    }

    private T CreateNewObject()
    {
        T tmp = Object.Instantiate(_objectToPool, _parentObject);
        tmp.gameObject.SetActive(false);
        _poolObjects.Add(tmp);
        return tmp;
    }
}