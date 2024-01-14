using System;
using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rigidbody;
    private float _speedMultiplier = 1;
    private float _startSpeedMultiplier;
    private Vector3 _targetPosition;
    private bool _canFollowTargetPosition;

    private void Start()
    {
        _startSpeedMultiplier = _speedMultiplier;
    }

    void Update()
    {
        if (_canFollowTargetPosition)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _speed * _speedMultiplier * Time.deltaTime);
        }
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }

    public void SetCanFollowTargetPosition(bool canFollow)
    {
        _canFollowTargetPosition = canFollow;
    }

    public Rigidbody SetRigidbody()
    {
        return _rigidbody;
    }

    public void SetSpeedMultiplier(float index)
    {
        _speedMultiplier = _startSpeedMultiplier - (index / 16);
        _speedMultiplier = Mathf.Clamp(_speedMultiplier, 0.25f, 1f);
        Debug.Log($"{transform.parent.gameObject.name} speed multiplier {index}" );
        
    }
}
