using UnityEngine;

public class Inhibitor : Effector
{
    public override void Init()
    {

    }

    void FixedUpdate() => transform.Translate(Vector2.left * ComplexityController.CurrentSpeedObstacles, Space.World);
}
