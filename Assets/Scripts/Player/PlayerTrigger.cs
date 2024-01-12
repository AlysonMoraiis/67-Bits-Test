using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Npc"))
        {
            _animator.SetTrigger("Punch");
        }
    }
}
