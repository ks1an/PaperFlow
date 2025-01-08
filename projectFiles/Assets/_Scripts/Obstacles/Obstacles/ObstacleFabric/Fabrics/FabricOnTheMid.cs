
using UnityEngine;

public class FabricOnTheMid : ObstaclesFabric
{
    int _currentObsTypeNum;
    int _currentObsSubTypeNum;
    int _currentObsNum;

    Vector3 _rightSpawn;

    RandomNumberGenerator _random;
    Transform _container;
    BaseDataObsFabrics _db;

    #region Set

    public override void SetFabricBaseData(BaseDataObsFabrics db, Transform container)
    {
        _db = db;
        _container = container;
        _random = RandomNumberGenerator.GetInstance();
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

    public override int GetDefaultFabricChance()
    {
        return _db.chance;
    }
    #endregion

    public override void CreateObjByRandom()
    {
        _currentObsTypeNum = SelectRandomType();
        _currentObsSubTypeNum = SelectRandomSupType();
        _currentObsNum = SelectRandomObs();


        var linkToCurObs = _db.obstacles.types[_currentObsTypeNum].SubTypes[_currentObsSubTypeNum].obs[_currentObsNum];

        #region Create

        GameObject prefab = linkToCurObs.prefab;
        Vector3 pos = linkToCurObs.position + _rightSpawn +
            new Vector3(0, _random.RangeFloat(linkToCurObs.minHeight, linkToCurObs.maxHeight));  //TODO: Check rig or left spawn

        var go = GameObject.Instantiate(prefab, pos, CachedMath.QuaternionIdentity, _container);
        #endregion

        #region PostCreat

        go.transform.Rotate(linkToCurObs.rotation);
        if (linkToCurObs.rotateYOnSpawn && _random.CheckChance(50))
            go.transform.Rotate(CachedMath.Vector2_0_180);
        #endregion


        var obs = go.GetComponent<Obstacle>();
        obs.Init(_db.followVector);
    }

    #region SelectByRandom
    int SelectRandomType()
    {
        while (true)
        {
            int num = _random.Range(0, _db.obstacles.types.Length);
            if (_random.CheckChance(_db.obstacles.types[num].chance)) return num;
        }
    }

    int SelectRandomSupType()
    {
        while (true)
        {
            int num = _random.Range(0, _db.obstacles.types[_currentObsTypeNum].SubTypes.Length);
            if (_random.CheckChance(_db.obstacles.types[_currentObsTypeNum].SubTypes[num].chance)) return num;
        }
    }

    int SelectRandomObs()
    {
        int num = _random.Range(0, _db.obstacles.types[_currentObsTypeNum].SubTypes[_currentObsSubTypeNum].obs.Length);
        return num;
    }
    #endregion
}
