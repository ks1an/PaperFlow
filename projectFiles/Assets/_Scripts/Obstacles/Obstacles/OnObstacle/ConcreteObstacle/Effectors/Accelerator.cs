using UnityEngine;

public sealed class Accelerator : Effector
{
    Vector2 _followPos;

    public override void Init(Vector2 follow)
    {
        _followPos = new Vector2(transform.position.x + follow.x, transform.position.y + follow.y);
    }

    void Update() => transform.position = Vector2.MoveTowards(transform.position, _followPos, ComplexitySettingsInProcedure.CurrentComplexitySpeed * Time.deltaTime);
}
