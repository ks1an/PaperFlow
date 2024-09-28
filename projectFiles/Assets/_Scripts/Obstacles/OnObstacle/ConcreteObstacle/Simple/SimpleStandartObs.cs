using UnityEngine;

public class SimpleStandartObs : Simple
{
    public override void Init()
    {

    }

    void FixedUpdate() => transform.Translate(Vector2.left * ComplexityController.CurrentSpeedObstacles, Space.World);
}
