using System;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public event Action<bool> OnSalesAreaTrigger;
    public event Action<GameObject> OnBodyTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Npc"))
        {
            _animator.SetTrigger("Punch");
        }

        else if (other.gameObject.CompareTag("SalesArea"))
        {
            OnSalesAreaTrigger?.Invoke(true);
        }

        else if (other.gameObject.CompareTag("Body"))
        {
            OnBodyTrigger?.Invoke(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SalesArea"))
        {
            OnSalesAreaTrigger?.Invoke(false);
        }
    }
}
