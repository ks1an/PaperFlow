using UnityEngine;

public sealed class FollowPathState : FsmDynamicObstacleState
{
    DynamicObstacle _dynamicObs;
    GameObject _body;
    Rigidbody2D _rb;

    public FollowPathState(FsmDynamicObstacle fsm, DynamicObstacle dynamicObs, GameObject body, Rigidbody2D rb) : base(fsm)
    {
        _dynamicObs = dynamicObs;
        _body = body;
        _rb = rb;
    }

    public override void Enter()
    {
        base.Enter();

        if (_dynamicObs.path == null)
        {
            DebuginggManager.LogError("Path == null! GameObject: " + _body.name);
            //TODO: To Die
            return;
        }

        _dynamicObs.pointInPath = _dynamicObs.path.GetNextPathPoint();
        _dynamicObs.pointInPath.MoveNext();

        if (_dynamicObs.pointInPath.Current == null)
        {
            DebuginggManager.LogError("PointPath == null! GameObject: " + _body.name);
            //TODO: To Die
            return;
        }

        _body.transform.position = _dynamicObs.pointInPath.Current.position;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_dynamicObs.pointInPath == null || _dynamicObs.pointInPath.Current == null) return;
        if (_body == null) return;

        if(_dynamicObs.pointInPath.Current.position.y > _body.transform.position.y)
        {
            Vector2 toTarget = (_dynamicObs.pointInPath.Current.position - _body.transform.position).normalized;
            _rb.velocity = toTarget * _dynamicObs.speed;
        }

        var distanceSquare = (_body.transform.position - _dynamicObs.pointInPath.Current.position).sqrMagnitude;

        if (distanceSquare < _dynamicObs.maxDistance * _dynamicObs.maxDistance)
            _dynamicObs.pointInPath.MoveNext();

        if (_rb.velocity.y != 0)
            _body.transform.eulerAngles = new Vector3(_rb.velocity.y * 2 + 60, 0, 90);
    }
}
