using UnityEngine;

public class SimpleDecorObjects : SimpleDecors
{
    [SerializeField, Range(0,1)] float multiplierSpeedX;
    Vector2 _followPos;

    public override void Init()
    {
        _followPos = new Vector2(-100, transform.position.y);
    }

    void Update() => transform.position = Vector2.MoveTowards(transform.position, _followPos, ComplexityController.CurrentSpeedObstacles * multiplierSpeedX * Time.deltaTime);
}
