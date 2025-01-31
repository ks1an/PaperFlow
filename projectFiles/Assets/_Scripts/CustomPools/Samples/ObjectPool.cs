using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : IPool<T> where T : MonoBehaviour, IPoolable<T>
{
    GameObject _prefab;
    Transform _myContainer;
    Stack<T> _pooledObjects = new();

    public ObjectPool(GameObject pooledObject, Transform container, int numToSpawn = 0)
    {
        _prefab = pooledObject;
        _myContainer = container;
        Spawn(numToSpawn);
    }

    #region Public func

    public T Get()
    {
        T t;
        if( _pooledObjects.Count > 0)
            t = _pooledObjects.Pop();
        else
            t = GameObject.Instantiate(_prefab, _myContainer).GetComponent<T>();

        t.gameObject.SetActive(true);
        t.Init(Release);

        return t;
    }

    public void Release(T t)
    {
        _pooledObjects.Push(t);
        t.gameObject.SetActive(false);
    }
    #endregion

    void Spawn(int num)
    {
        T t;
        for (int i = 0; i < num; i++)
        {
            t = GameObject.Instantiate(_prefab, _myContainer).GetComponent<T>();
            _pooledObjects.Push(t);
            t.gameObject.SetActive(false);
        }
    }
}
