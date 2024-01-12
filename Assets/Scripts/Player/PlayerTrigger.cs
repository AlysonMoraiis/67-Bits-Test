using System;
using UnityEngine;


public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public event Action<bool> OnSalesAreaCollision;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Npc"))
        {
            _animator.SetTrigger("Punch");
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("SalesArea"))
        {
            OnSalesAreaCollision?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SalesArea"))
        {
            OnSalesAreaCollision?.Invoke(false);
        }
    }
}
