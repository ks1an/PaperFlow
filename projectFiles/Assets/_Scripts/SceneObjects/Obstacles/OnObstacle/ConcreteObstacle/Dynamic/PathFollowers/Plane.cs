using System.Collections.Generic;
using UnityEngine;

public class Plane : PathFollower
{
    [SerializeField] private float _speed = 1f, _maxDistance = 1f;
    [SerializeField] private bool _canMoveToLeft = true;
    [SerializeField] private Transform _body;
    [SerializeField] private PathCreator _path;

    IEnumerator<Transform> _pointInPath;
    Vector2 _followMovePos;

    public override void Init()
    {
        _followMovePos = new Vector2(-100, 0 + transform.position.y);

        #region Init Path
        if (_path == null)
        {
            Debug.LogError("Path == null! GameObject: " + gameObject.name);
            Destroy(gameObject);
            return;
        }

        _pointInPath = _path.GetNextPathPoint();
        _pointInPath.MoveNext();

        if (_pointInPath.Current == null)
        {
            Debug.LogError("PointPath == null! GameObject: " + gameObject.name);
            Destroy(gameObject);
            return;
        }

        _body.transform.position = _pointInPath.Current.position;
        #endregion
    }

    private void Update()
    {
        if(_canMoveToLeft)
            transform.position = Vector2.MoveTowards(transform.position, _followMovePos, ComplexityController.CurrentSpeedObstacles * Time.deltaTime);

        if (_pointInPath == null || _pointInPath.Current == null || _body == null) return;
        _body.position = Vector2.MoveTowards(_body.position, _pointInPath.Current.position, _speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (_pointInPath == null || _pointInPath.Current == null || _body == null) return;

        var distanceSquare = (_body.position - _pointInPath.Current.position).sqrMagnitude;
        if (distanceSquare < _maxDistance * _maxDistance)
            _pointInPath.MoveNext();
    }
}
