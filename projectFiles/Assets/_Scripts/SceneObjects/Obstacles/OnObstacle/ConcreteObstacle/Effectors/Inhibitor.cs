using UnityEngine;

public sealed class Inhibitor : Effector
{
    Vector2 _followPos;

    public override void Init()
    {
        _followPos = new Vector2(-100, transform.position.y);
    }

    void Update() => transform.position = Vector2.MoveTowards(transform.position, _followPos, ComplexityController.CurrentComplexitySpeed * Time.deltaTime);
}
