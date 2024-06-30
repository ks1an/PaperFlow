using System;
using UnityEngine;

public sealed class RightObstaclesTrigger : MonoBehaviour
{
    public static Action onObstacleExitRightTrigger;

    private void OnTriggerExit2D(Collider2D collision)  // only CompareTage("Obstacle"). Check collider layers.
    {
        onObstacleExitRightTrigger?.Invoke(); 
    }
}
