using UnityEngine;

public class ConstructionStandartObs : Construction
{
    public override void Init()
    {

    }

    void FixedUpdate() => transform.Translate(Vector2.left * ComplexityController.CurrentSpeedObstacles, Space.World);
}
