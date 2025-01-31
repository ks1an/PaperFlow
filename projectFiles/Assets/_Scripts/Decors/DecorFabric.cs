using UnityEngine;

public sealed class DecorFabric
{
    int _currentObjCategoryNum;
    int _currentObjTypeNum;
    int _currentObjNum;

    Vector2 _rightSpawn;

    readonly RandomNumberGenerator _random = RandomNumberGenerator.GetInstance();
    Transform _container;
    BaseDataDecorFabric _db;

    #region Set

    public void SetFabricBaseData(BaseDataDecorFabric db, Transform container)
    {
        _db = db;
        _container = container;
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

        #region Create

        var linkToCurObs = _db.decorObjects.categories[_currentObjCategoryNum].types[_currentObjTypeNum].obj[_currentObjNum];

        GameObject prefab = linkToCurObs.prefab;
        Vector3 pos = linkToCurObs.position +
            new Vector3(_rightSpawn.x, _random.RangeFloat(linkToCurObs.minHeight, linkToCurObs.maxHeight) + _rightSpawn.y);

        var go = GameObject.Instantiate(prefab, pos, CachedMath.QuaternionIdentity, _container);
        #endregion

        #region PostCreate

        go.transform.Rotate(linkToCurObs.rotation);
        if (linkToCurObs.rotateYOnSpawn && _random.CheckChance(50))
            go.transform.Rotate(CachedMath.Vector2_0_180);
        #endregion


        var obs = go.GetComponent<DecorObject>();
        obs.Init();
    }

    #region SelectByRandom
    int SelectRandomType()
    {
        Category[] categories = _db.decorObjects.categories;
        while (true)
        {
            int num = _random.Range(0, categories.Length);
            if (_random.CheckChance(categories[num].chance)) return num;
        }
    }

    int SelectRandomSupType()
    {
        TypeLayer[] types = _db.decorObjects.categories[_currentObjCategoryNum].types;
        while (true)
        {
            int num = _random.Range(0, types.Length);
            if (_random.CheckChance(types[num].chance)) return num;
        }
    }

    int SelectRandomObs()
    {
        int num = _random.Range(0, _db.decorObjects.categories[_currentObjCategoryNum].types[_currentObjTypeNum].obj.Length);
        return num;
    }
    #endregion
}
