
using UnityEngine;

public abstract class ObstaclesFabric
{
    public abstract void SetFabricBaseData(BaseDataObsFabrics db, Transform container);
    public abstract void SetFabricSpawnsPos(Vector3 rig, Vector3 left);
    public abstract void CreateObjByRandom();

    public abstract float GetDelayTime();
    public abstract int GetDefaultFabricChance();
}
