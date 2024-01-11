using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;

    private Vector3 _directionInput;

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        Vector3 movement = new Vector3(_directionInput.x, 0f, _directionInput.y);
        _controller.Move(movement * (Time.deltaTime * _speed));

        UpdateAnimator(movement);
    }

    private void HandleRotation()
    {
        if (_directionInput.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(_directionInput.x, _directionInput.y) * Mathf.Rad2Deg;
            Quaternion toRotation = Quaternion.AngleAxis(angle, transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 15f);
        }
    }

    private void UpdateAnimator(Vector3 movement)
    {
        _animator.SetBool("Running", movement.magnitude > 0f);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _directionInput = context.ReadValue<Vector2>();
    }
}