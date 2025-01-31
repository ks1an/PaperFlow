using System.Collections.Generic;
using UnityEngine;

public sealed class SimpleObstacle : Simple
{
    [SerializeField] List<HaveBroken> _myDestroyDetails = new();
    Vector2 _followPos;

    public override void Init(Vector2 follow)
    {
        _followPos = new Vector2(transform.position.x + follow.x, transform.position.y + follow.y);
        for (int i = 0; i < _myDestroyDetails.Count; i++)
            _myDestroyDetails[i].gameObject.SetActive(true);
    }

    void Update() => transform.position = Vector2.MoveTowards(transform.position, _followPos, ComplexitySettingsInProcedure.CurrentComplexitySpeed * Time.deltaTime);
}
