using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BaseDataObsFabrics", order = 1)]
public class BaseDataObsFabrics : ScriptableObject
{
    public string nameFabric;
    [Range(0, 100)] public int chance;

    public ObstaclesList obstacles = new();
}

#region ObstaclesList

[Serializable]
public class ObstaclesList
{
    public Types[] types = new Types[1];
}
[Serializable]

public class Types
{
    public string nameType;
    [Range(0, 100)] public int chance;

    public SubTypes[] SubTypes = new SubTypes[1];
}
[Serializable]

public class SubTypes
{
    public string nameSubType;
    [Range(0, 100)] public int chance;

    public Obstacles[] obs = new Obstacles[1];
}
[Serializable]

public class Obstacles
{
    public string nameId;
    public GameObject prefab;

    [Header("Transform")]
    public Vector3 position;
    public Vector3 rotation;
    public float maxHeight;
    public float minHeight;

    [Space(10)]
    public bool rotateYOnSpawn = false;
    public float spawnDelay = 1.25f;
}
#endregion
