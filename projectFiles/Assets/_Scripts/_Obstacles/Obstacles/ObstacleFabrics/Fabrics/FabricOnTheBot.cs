using UnityEngine;

public sealed class FabricOnTheBot : ObstaclesFabric
{
    int _currentObsTypeNum;
    int _currentObsSubTypeNum;
    int _currentObsNum;

    Vector2 _rightSpawn;

    readonly RandomNumberGenerator _random = RandomNumberGenerator.GetInstance();
    Transform _container;
    BaseDataObsFabrics _db;
    ObstaclePools _simpleObsPools;

    #region Set

    public override void SetFabricBaseData(BaseDataObsFabrics db, Transform container, ObstaclePools mySimplePools)
    {
        _db = db;
        _container = container;
        _simpleObsPools = mySimplePools;
    }

    public override void SetFabricSpawnsPos(Vector3 rightSpawn, Vector3 left)
    {
        _rightSpawn = rightSpawn;
    }
    #endregion

    #region Get
    public override float GetDelayTime()
    {
        return _db.obstacles.types[_currentObsTypeNum].SubTypes[_currentObsSubTypeNum].obs[_currentObsNum].spawnDelay;
    }

    public override int GetDefaultFabricChance() { return _db.chance; }
    #endregion

    public override void CreateObjByRandom()
    {
        #region GetObjFromDB

        _currentObsTypeNum = SelectRandomType();
        _currentObsSubTypeNum = SelectRandomSupType();
        _currentObsNum = SelectRandomObs();
        var linkToCurObs = _db.obstacles.types[_currentObsTypeNum].SubTypes[_currentObsSubTypeNum].obs[_currentObsNum];
        #endregion

        #region Before create

        GameObject prefab = linkToCurObs.prefab;
        Vector3 pos = linkToCurObs.position +
            new Vector3(_rightSpawn.x, _random.RangeFloat(linkToCurObs.minHeight, linkToCurObs.maxHeight) + _rightSpawn.y);
        #endregion

        #region Create

        GameObject go = null;
        if (linkToCurObs.isPoolable)
        {
            if (linkToCurObs.pool == null)
            {
                linkToCurObs.pool = _simpleObsPools.FindAndGetPool(prefab);
                go = _simpleObsPools.GetObsFromPool(linkToCurObs.pool);
            }
            else
                go = _simpleObsPools.GetObsFromPool(linkToCurObs.pool);
        }

        if (go == null)
            go = GameObject.Instantiate(prefab, pos, CachedMath.QuaternionIdentity, _container);
        else
            go.transform.SetPositionAndRotation(pos, CachedMath.QuaternionIdentity);
        #endregion

        #region PostCreate

        go.transform.Rotate(linkToCurObs.rotation);
        if (linkToCurObs.rotateYOnSpawn && _random.CheckChance(50))
            go.transform.Rotate(CachedMath.Vector2_0_180);

        go.GetComponent<Obstacle>().Init(_db.followVector);
        #endregion
    }

    #region SelectByRandom
    int SelectRandomType()
    {
        Types[] types = _db.obstacles.types;
        while (true)
        {
            int num = _random.Range(0, types.Length);
            if (_random.CheckChance(types[num].chance))
                return num;
        }
    }

    int SelectRandomSupType()
    {
        SubTypes[] subTypes = _db.obstacles.types[_currentObsTypeNum].SubTypes;
        while (true)
        {
            int num = _random.Range(0, subTypes.Length);
            if (_random.CheckChance(subTypes[num].chance))
                return num;
        }
    }

    int SelectRandomObs()
    {
        int num = _random.Range(0, _db.obstacles.types[_currentObsTypeNum].SubTypes[_currentObsSubTypeNum].obs.Length);
        return num;
    }
    #endregion
}
