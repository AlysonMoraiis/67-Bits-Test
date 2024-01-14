using System;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private Animator _animator;

    #region Events

    public event Action<bool> OnSalesAreaTrigger;
    public event Action<bool> OnShopAreaTrigger;
    public event Action<Body> OnBodyTrigger;

    #endregion


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

        else if (other.gameObject.CompareTag("ShopArea"))
        {
            OnShopAreaTrigger?.Invoke(true);
        }

        if (other.gameObject.TryGetComponent(out Body body))
        {
            OnBodyTrigger?.Invoke(body);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SalesArea"))
        {
            OnSalesAreaTrigger?.Invoke(false);
        }

        else if (other.gameObject.CompareTag("ShopArea"))
        {
            OnShopAreaTrigger?.Invoke(false);
        }
    }
}
