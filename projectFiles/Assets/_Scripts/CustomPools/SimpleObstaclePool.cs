using UnityEngine;
using UnityEngine.Pool;

public class SimpleObstaclePool
{
    ObjectPool<SimpleObstacle> _pool;
    SimpleObstacle _prefab;

    public SimpleObstaclePool(SimpleObstacle prefab, int prewarObjectsCount)
    {
        _prefab = prefab;
        _pool = new ObjectPool<SimpleObstacle>(OnCreateBroken, OnGetBroken, OnRelease, OnBrokenDestroy);
    }

    public SimpleObstacle Get()
    {
        var obj = _pool.Get();
        return obj;
    }

    public void Release(SimpleObstacle obj)
    {
        _pool.Release(obj);
    }

    #region OnAction

    SimpleObstacle OnCreateBroken()
    {
        return GameObject.Instantiate(_prefab);
    }

    void OnGetBroken(SimpleObstacle obj)
    {
        obj.gameObject.SetActive(true);
    }

    void OnRelease(SimpleObstacle obj)
    {
        obj.gameObject.SetActive(false);
    }

    void OnBrokenDestroy(SimpleObstacle obj)
    {
        GameObject.Destroy(obj);
    }
    #endregion
}
