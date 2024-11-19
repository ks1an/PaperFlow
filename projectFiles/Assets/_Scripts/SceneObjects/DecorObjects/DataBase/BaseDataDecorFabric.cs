using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BaseDataDecorFabric", order = 2)]
public class BaseDataDecorFabric : ScriptableObject
{
    public DecorObjectList decorObjects = new();
}

#region DecorObjectList

[Serializable]
public class DecorObjectList
{
    public Category[] categories = new Category[1];
}
[Serializable]

public class Category
{
    public string nameCategory;
    [Range(0, 100)] public int chance;

    public TypeLayer[] types = new TypeLayer[1];
}
[Serializable]

public class TypeLayer
{
    public string nameType;
    [Range(0, 100)] public int chance;

    public DecorObjects[] obj = new DecorObjects[1];
}
[Serializable]

public class DecorObjects
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
