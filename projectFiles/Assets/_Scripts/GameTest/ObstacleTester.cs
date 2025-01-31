#if UNITY_EDITOR
using UnityEngine;


public sealed class ObstacleTester : MonoBehaviour
{
    [SerializeField] Vector2 _followVector2; 
    Obstacle _obs;

    private void OnEnable()
    {
        _obs = GetComponent<Obstacle>();

        if(!_obs.enabled)
            _obs.enabled = true;

        _obs.Init(_followVector2);
    }
}
#endif
