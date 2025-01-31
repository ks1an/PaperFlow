using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class ObstaclePools : MonoBehaviour
{
    [SerializeField] Transform _container;
    [SerializeField] List<ObstacleResume> _obstacleResumeList = new();
    
    void Awake()
    {
        for (int obstacleIndex = 0;  obstacleIndex < _obstacleResumeList.Count; obstacleIndex++)
            _obstacleResumeList[obstacleIndex].CreatePool(_container);
    }

    #region Get

    public ObjectPool<PoolObject> FindAndGetPool(GameObject prefab)
    {
        for (int i = 0; i < _obstacleResumeList.Count; i++)
        {
            var resume = _obstacleResumeList[i];
            if (prefab == resume.prefab)
                return resume.pool;
        }

        return null;
    }

    public GameObject GetObsFromPool(ObjectPool<PoolObject> pool) { return GetObject(pool).gameObject; }

    PoolObject GetObject(ObjectPool<PoolObject> curPool) { return curPool.Get(); }
    #endregion

    [Serializable]
    public class ObstacleResume
    {
        public string name;
        public GameObject prefab;
        public int countToStart;

        public ObjectPool<PoolObject> pool;

        public void CreatePool(Transform container)
        {
            pool = new ObjectPool<PoolObject>(prefab, container, countToStart);
        }
    }
}
