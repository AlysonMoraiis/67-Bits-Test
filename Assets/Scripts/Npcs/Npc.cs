using System;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [Header("Components")] [SerializeField]
    private CapsuleCollider _capsuleCollider;

    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _hipsRigidbody;

    [Header("Death Settings")] [SerializeField]
    private float _deathForceMultiplier = 35f;

    private void OnEnable()
    {
        SetDefaultParameters();
    }

    private void SetDefaultParameters()
    {
        _capsuleCollider.enabled = true;
        _animator.enabled = true;
        _hipsRigidbody.isKinematic = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HandleDeath(other.transform.position);
        }
    }

    private void HandleDeath(Vector3 playerPosition)
    {
        DisableComponents();
        ApplyDeathForce(playerPosition);
    }

    private void DisableComponents()
    {
        _capsuleCollider.enabled = false;
        _animator.enabled = false;
    }

    private void ApplyDeathForce(Vector3 playerPosition)
    {
        Vector3 forceDirection = (transform.position - playerPosition).normalized;
        ApplyForce(forceDirection * _deathForceMultiplier);
    }

    private void ApplyForce(Vector3 force)
    {
        _hipsRigidbody.AddForce(force, ForceMode.VelocityChange);
    }
}
