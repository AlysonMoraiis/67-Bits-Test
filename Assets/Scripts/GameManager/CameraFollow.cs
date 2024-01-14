using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothTime;

    private Vector3 _offset;
    private Vector3 _currentVelocity;

    private void Awake()
    {
        CalculateOffset();
    }

    private void CalculateOffset()
    {
        _offset = transform.position - _target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = _target.position + _offset;
        SmoothlyFollowTarget(targetPosition);
    }

    private void SmoothlyFollowTarget(Vector3 targetPosition)
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, _smoothTime);
    }
}
