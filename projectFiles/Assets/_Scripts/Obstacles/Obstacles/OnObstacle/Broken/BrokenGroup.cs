using UnityEngine;

public sealed class BrokenGroup : MonoBehaviour
{
    [SerializeField] float _torque, _knockbackSpeedX, _knockbackSpeedY;
    [SerializeField, Range(0.1f,1)] float _minRndMult, _maxRndMult;
    [SerializeField] Rigidbody2D[] _rbodies;

    Vector2 _knockBackVector;

    RandomNumberGenerator _random = RandomNumberGenerator.GetInstance();

    void OnEnable()
    {
        for (int i = 0; i < _rbodies.Length; i++)
        {
            Transform myBrokeDetail = _rbodies[i].gameObject.transform;
            _rbodies[i].gameObject.transform.SetLocalPositionAndRotation(myBrokeDetail.localPosition, myBrokeDetail.localRotation);
        }

        _knockBackVector = new Vector2(_knockbackSpeedX, _knockbackSpeedY);
        for (int i = 0; i < _rbodies.Length; i++)
        {
            _rbodies[i].velocity = _knockBackVector * _random.RangeFloat(_minRndMult, _maxRndMult);
            _rbodies[i].AddTorque(_torque, ForceMode2D.Impulse);
        }
    }
}
