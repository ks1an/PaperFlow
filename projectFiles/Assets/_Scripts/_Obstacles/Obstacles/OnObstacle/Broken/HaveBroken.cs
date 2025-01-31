using System.Collections.Generic;
using UnityEngine;

public sealed class HaveBroken : MonoBehaviour
{
    [SerializeField] List<BrokenGroup> _myBrokenGroupsPrefabs;
    readonly RandomNumberGenerator _random = RandomNumberGenerator.GetInstance();

    void OnDisable()
    {
        if (gameObject.transform.parent.gameObject.activeSelf)
        {
            var curGroup = _myBrokenGroupsPrefabs[_random.Range(0, _myBrokenGroupsPrefabs.Count)];

            var obj = Instantiate(curGroup, parent: gameObject.transform.parent, position: gameObject.transform.position, rotation: CachedMath.QuaternionIdentity);
            obj.gameObject.SetActive(true);
        }
    }
}
