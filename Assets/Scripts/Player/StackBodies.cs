using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class StackBodies : MonoBehaviour
{
    [SerializeField] private Transform _stackTransform;
    [SerializeField] private float _bodyHeight = 5f;
    [SerializeField] private PlayerTrigger _playerTrigger;
    [SerializeField] private float _depositDelay;
    [SerializeField] private GameData _gameData;

    public event Action OnBodyAction;

    private bool _canDeposit;

    private readonly List<GameObject> _bodies = new List<GameObject>();

    private void OnEnable()
    {
        _playerTrigger.OnSalesAreaTrigger += DepositBodyOnSalesArea;
        _playerTrigger.OnBodyTrigger += AddToBodiesList;
    }

    private void OnDisable()
    {
        _playerTrigger.OnSalesAreaTrigger -= DepositBodyOnSalesArea;
        _playerTrigger.OnBodyTrigger -= AddToBodiesList;
    }

    private void AddToBodiesList(GameObject body)
    {
        if (_bodies.Count >= _gameData.LoadLimit)
        {
            return;
        }
        _bodies.Add(body.gameObject);
        _gameData.CurrentLoad++;
        OnBodyAction?.Invoke();

        SetBodyPosition(body.gameObject);
    }

    private void SetBodyPosition(GameObject body)
    {
        body.transform.rotation = Quaternion.Euler(90f, Random.Range(0, 200), transform.rotation.z);

        Rigidbody bodyRigidbody = body.GetComponent<Rigidbody>();
        if (bodyRigidbody != null)
        {
            bodyRigidbody.isKinematic = true;
        }

        StartCoroutine(FollowPlayerTransform());
    }

    private void DepositBodyOnSalesArea(bool canDeposit)
    {
        if (!canDeposit)
        {
            _canDeposit = false;
            return;
        }

        _canDeposit = true;
        
        StartCoroutine(MoveBodyToSalesArea());
    }

    private IEnumerator MoveBodyToSalesArea()
    {
        while (_canDeposit && _bodies.Count > 0)
        {
            _bodies.Last().transform.parent.gameObject.SetActive(false);
            _bodies.Remove(_bodies.Last());
            _gameData.Money += _gameData.Npc01Value;
            _gameData.CurrentLoad--;
            OnBodyAction?.Invoke();
            yield return new WaitForSeconds(_depositDelay);
        }

        yield return null;
    }

    private IEnumerator FollowPlayerTransform()
    {
        while (true)
        {
            for (int i = 0; i < _bodies.Count; i++)
            {
                float newY = _stackTransform.position.y + _bodyHeight * i;
                var position = _stackTransform.transform.position;
                _bodies[i].transform.position = new Vector3(position.x, newY, position.z);
                _bodies[i].transform.parent.position = new Vector3(position.x, transform.position.y, position.z);
            }

            yield return null;
        }
    }
}
