using System.Collections.Generic;
using UnityEngine;

public sealed class FollowPath : MonoBehaviour
{
    enum MovementType
    {
        Moveing,
        Lerping
    }

    [SerializeField] MovementType _type = MovementType.Moveing;
    [SerializeField] PathCreator _path;
    [SerializeField] float _speed = 1f, _maxDistance = 1f;

    IEnumerator<Transform> _pointInPath;

    private void Start()
    {
        if (_path == null)
        {
            DebuginggManager.LogError("Path == null! GameObject: " + gameObject.name);
            Destroy(gameObject);
            return;
        }

        _pointInPath = _path.GetNextPathPoint();
        _pointInPath.MoveNext();

        if (_pointInPath.Current == null)
        {
            DebuginggManager.LogError("PointPath == null! GameObject: " + gameObject.name);
            Destroy(gameObject);
            return;
        }

        transform.position = _pointInPath.Current.position;
    }

    private void Update()
    {
        if (_pointInPath == null || _pointInPath.Current == null) return;

        if (_type == MovementType.Moveing)
            transform.position = Vector2.MoveTowards(transform.position, _pointInPath.Current.position,
                Time.deltaTime * _speed);
        else if (_type == MovementType.Lerping)
            transform.position = Vector2.Lerp(transform.position, _pointInPath.Current.position,
                Time.deltaTime * _speed);

        var distanceSquare = (transform.position - _pointInPath.Current.position).sqrMagnitude;

        if (distanceSquare < _maxDistance * _maxDistance)
            _pointInPath.MoveNext();
    }
}
