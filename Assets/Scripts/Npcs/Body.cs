using System;
using UnityEngine;

public class Body : MonoBehaviour
{
    [Header("Movement Settings")] 
    [SerializeField] private float _speed;

    [SerializeField] private float _speedMultiplier = 1;

    [Header("References")] 
    [SerializeField] private Rigidbody _rigidbody;

    private float _startSpeedMultiplier;
    private Vector3 _targetPosition;
    private bool _canFollowTargetPosition;
    private PlayerDirection _playerDirection;
    private Quaternion _startTransformRotation;
    
    private void Start()
    {
        _startSpeedMultiplier = _speedMultiplier;
    }

    void Update()
    {
        if (_canFollowTargetPosition)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _speed * _speedMultiplier * Time.deltaTime);
            
            
            // Debug.Log("player direciton " + _playerDirection);
            //
            // if (_playerDirection == PlayerDirection.forward)
            // {
            //     transform.parent.rotation = Quaternion.identity;
            // }
            // else if (_playerDirection == PlayerDirection.right)
            // {
            //     transform.parent.rotation = Quaternion.Euler(new Vector3(0, transform.parent.rotation.y, _startTransformRotation.z - 35));
            // }
            // else
            // {
            //     transform.parent.rotation = Quaternion.Euler(new Vector3(0, transform.parent.rotation.y, _startTransformRotation.z + 35));
            // }
            
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

    public Rigidbody GetRigidbody()
    {
        return _rigidbody;
    }

    public void SetSpeedMultiplier(float index)
    {
        _speedMultiplier = _startSpeedMultiplier - (index / 17);
        _speedMultiplier = Mathf.Clamp(_speedMultiplier, 0.25f, 1f);
        Debug.Log($"{transform.parent.gameObject.name} speed multiplier {index}" );
    }
    
    public void SetPlayerDirection(PlayerDirection getPlayerDirection)
    {
        _playerDirection = getPlayerDirection;
    }

    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
        _startTransformRotation = transform.parent.rotation;
    }
}
