using System.Collections.Generic;
using UnityEngine;

public class Plane : PathFollower
{
    [SerializeField] private float _speed = 1f, _maxDistance = 1f;
    [SerializeField] private Transform _body;
    [SerializeField] private PathCreator _path;

    IEnumerator<Transform> _pointInPath;

    public override void Init()
    {
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

    private void FixedUpdate()
    {
        this.transform.Translate(Vector2.left * ComplexityController.CurrentSpeedObstacles, Space.World);

        if (_pointInPath == null || _pointInPath.Current == null || _body == null) return;
        #region Go To pointInPath

        _body.transform.position = Vector2.MoveTowards(_body.transform.position, _pointInPath.Current.position, _speed);

        var distanceSquare = (_body.transform.position - _pointInPath.Current.position).sqrMagnitude;
        if (distanceSquare < _maxDistance * _maxDistance)
            _pointInPath.MoveNext();
        #endregion
    }
}
