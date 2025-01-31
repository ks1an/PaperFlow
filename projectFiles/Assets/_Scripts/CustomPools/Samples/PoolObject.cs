using System;
using UnityEngine;

public sealed class PoolObject : MonoBehaviour, IPoolable<PoolObject>
{
    Action<PoolObject> _returnToPool;

    public void Init(Action<PoolObject> returnToPool)
    {
        _returnToPool = returnToPool;
    }

    public void ReturnToPool()
    {
        _returnToPool?.Invoke(this);
    }

    void OnDisable()
    {
        ReturnToPool();
    }
}
