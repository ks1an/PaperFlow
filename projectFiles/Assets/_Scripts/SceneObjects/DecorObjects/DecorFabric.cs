using UnityEngine;

public sealed class DecorFabric
{
    int _currentObjCategoryNum;
    int _currentObjTypeNum;
    int _currentObjNum;

    Vector3 _rightSpawn;

    RandomNumberGenerator _random;
    Transform _container;
    BaseDataDecorFabric _db;

    #region Set

    public void SetFabricBaseData(BaseDataDecorFabric db, Transform container)
    {
        _db = db;
        _container = container;
        _random = RandomNumberGenerator.GetInstance();
    }

    public void SetFabricSpawnsPos(Vector3 rightSpawn)
    {
        _rightSpawn = rightSpawn;
    }
    #endregion

    #region Get
    public float GetDelayTime()
    {
        return _db.decorObjects.categories[_currentObjCategoryNum].types[_currentObjTypeNum].obj[_currentObjNum].spawnDelay;
    }
    #endregion

   public void CreateObjByRandom()
    {
        _currentObjCategoryNum = SelectRandomType();
        _currentObjTypeNum = SelectRandomSupType();
        _currentObjNum = SelectRandomObs();


        var linkToCurObs = _db.decorObjects.categories[_currentObjCategoryNum].types[_currentObjTypeNum].obj[_currentObjNum];

        GameObject prefab = linkToCurObs.prefab;
        Vector3 pos = linkToCurObs.position + _rightSpawn +
            new Vector3(0, _random.RangeFloat(linkToCurObs.minHeight, linkToCurObs.maxHeight));

        var go = GameObject.Instantiate(prefab, pos, CachedMath.QuaternionIdentity, _container);

        go.transform.Rotate(linkToCurObs.rotation);
        if (linkToCurObs.rotateYOnSpawn && _random.CheckChance(50))
            go.transform.Rotate(CachedMath.Vector2_0_180);


        var obs = go.GetComponent<DecorObject>();
        obs.Init();
    }

    #region SelectByRandom
    int SelectRandomType()
    {
        while (true)
        {
            int num = _random.Range(0, _db.decorObjects.categories.Length);
            if (_random.CheckChance(_db.decorObjects.categories[num].chance)) return num;
        }
    }

    int SelectRandomSupType()
    {
        while (true)
        {
            int num = _random.Range(0, _db.decorObjects.categories[_currentObjCategoryNum].types.Length);
            if (_random.CheckChance(_db.decorObjects.categories[_currentObjCategoryNum].types[num].chance)) return num;
        }
    }

    int SelectRandomObs()
    {
        int num = _random.Range(0, _db.decorObjects.categories[_currentObjCategoryNum].types[_currentObjTypeNum].obj.Length);
        return num;
    }
    #endregion
}
