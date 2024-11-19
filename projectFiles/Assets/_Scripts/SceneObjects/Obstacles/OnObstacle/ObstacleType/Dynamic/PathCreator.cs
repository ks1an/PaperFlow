using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class PathCreator : MonoBehaviour
{
    [SerializeField] bool _isLoopPath;
    [SerializeField] bool _randomLoopOrNotPath;
    [SerializeField] bool _canReversePathElementsArray;
    [SerializeField] Transform[] _pathElements;

    int _movementDirection = 1;
    int _moveingToPoint = 0;

    private void Start()
    {
        if (_canReversePathElementsArray)
            if (Random.Range(0, 50) <= 50)
                Array.Reverse(_pathElements);

        if (_randomLoopOrNotPath)
        {
            if (Random.Range(0, 50) <= 50)
                _isLoopPath = true;
            else
                _isLoopPath = false;
        }
    }
    void OnDrawGizmos()
    {
        if (_pathElements == null || _pathElements.Length < 2) return;

        for (int i = 1; i < _pathElements.Length; i++)
            Gizmos.DrawLine(_pathElements[i - 1].position, _pathElements[i].position);

        if (_isLoopPath)
            Gizmos.DrawLine(_pathElements[0].position, _pathElements[_pathElements.Length - 1].position);
    }

    public IEnumerator<Transform> GetNextPathPoint()
    {
        if (_pathElements == null || _pathElements.Length < 1) yield break;

        while (true)
        {
            yield return _pathElements[_moveingToPoint];

            if (_pathElements.Length == 1) continue;

            if (!_isLoopPath)
            {
                if (_moveingToPoint <= 0)
                    _movementDirection = 1;
                else if (_moveingToPoint >= _pathElements.Length - 1)
                    _movementDirection = -1;
            }

            _moveingToPoint += _movementDirection;

            if (_isLoopPath)
            {
                if (_moveingToPoint >= _pathElements.Length)
                    _moveingToPoint = 0;
                else if (_moveingToPoint < 0)
                    _moveingToPoint = _pathElements.Length - 1;
            }

        }
    }
}
