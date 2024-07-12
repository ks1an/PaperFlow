using System.Collections.Generic;
using UnityEngine;

public sealed class DynamicObstacle : MonoBehaviour
{
    [Header("PathFollow")]
    public float speed = 1f, maxDistance = 1f;
    public PathCreator path;
    public IEnumerator<Transform> pointInPath;

    [Space(10)]
    [SerializeField] GameObject _body;

    FsmDynamicObstacle _fsm;
    Rigidbody2D _rb;

    private void Awake()
    {
       _rb = _body.GetComponent<Rigidbody2D>();

        _fsm = new FsmDynamicObstacle();

        _fsm.AddState(new FollowPathState(_fsm, this, _body, _body.GetComponent<Rigidbody2D>()));

        _fsm.SetState<FollowPathState>();
    }

    private void Update()
    {
        _fsm.Update();

        transform.position += ObstacleManager.CurrentSpeedObstacles * Time.deltaTime * Vector3.left;
    }
    private void FixedUpdate()
    {
        _fsm.LogicUpdate();
    }

}
