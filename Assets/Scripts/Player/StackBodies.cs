using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackBodies : MonoBehaviour
{
    [SerializeField] private Transform _stackTransform;
    [SerializeField] private float _bodyHeight = 0.7f;
    [SerializeField] private PlayerTrigger _playerTrigger;
    [SerializeField] private float _depositDelay;

    private List<GameObject> _bodies = new List<GameObject>();

    private void OnEnable()
    {
        _playerTrigger.OnSalesAreaCollision += DepositBodyOnSalesArea;
    }

    private void OnDisable()
    {
        _playerTrigger.OnSalesAreaCollision -= DepositBodyOnSalesArea;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            Transform bodyTransform = other.transform;
            bodyTransform.SetParent(transform);

            SetBodyPosition(bodyTransform.gameObject);

            _bodies.Add(other.gameObject);
        }
    }

    private void SetBodyPosition(GameObject body)
    {
        float newY = _stackTransform.position.y + _bodyHeight * _bodies.Count;
        body.transform.localPosition = new Vector3(0f, newY, 0f);
        body.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        Rigidbody bodyRigidbody = body.GetComponent<Rigidbody>();
        if (bodyRigidbody != null)
        {
            bodyRigidbody.isKinematic = true;
        }
    }

    private void DepositBodyOnSalesArea(bool canDeposit)
    {
        if (!canDeposit)
        {
            return;
        }

        StartCoroutine(MoveBodyToSalesArea());
    }

    private IEnumerator MoveBodyToSalesArea()
    {
        yield return new WaitForSeconds(_depositDelay);
    }
}
