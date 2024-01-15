using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class StackBodies : MonoBehaviour
{
    [Header("Stack Settings")] 
    [SerializeField] private Transform _stackTransform;
    [SerializeField] private float _bodyHeight = 5f;
    [SerializeField] private float _depositDelay = 0.4f;

    [Header("References")] [SerializeField]
    private PlayerTrigger _playerTrigger;

    [SerializeField] private GameData _gameData;

    #region Events

    public event Action OnBodyCollect;
    public event Action OnBodySale;

    #endregion

    private bool _canDeposit;
    private readonly List<Body> _bodies = new List<Body>();

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

    private void AddToBodiesList(Body body)
    {
        if (_bodies.Count >= _gameData.LoadLimit)
        {
            return;
        }

        _bodies.Add(body);
        _gameData.CurrentLoad++;
        OnBodyCollect?.Invoke();

        SetBodyPosition(body);
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

    private void SetBodyPosition(Body body)
    {
        Rigidbody bodyRigidbody = body.SetRigidbody();
        if (bodyRigidbody != null)
        {
            bodyRigidbody.isKinematic = true;
        }

        FollowPlayerTransform(body);
    }

    private IEnumerator MoveBodyToSalesArea()
    {
        while (_canDeposit && _bodies.Count > 0)
        {
            _bodies.Last().transform.parent.gameObject.SetActive(false);
            _bodies.Remove(_bodies.Last());

            _gameData.Money += _gameData.Npc01Value;
            _gameData.CurrentLoad--;

            OnBodySale?.Invoke();
            OnBodyCollect?.Invoke();

            yield return new WaitForSeconds(_depositDelay);
        }

        yield return null;
    }

    private void FollowPlayerTransform(Body body)
    {
        for (int i = 0; i < _bodies.Count; i++)
        {
            body.transform.rotation = Quaternion.Euler(90f, Random.Range(0, 200), transform.rotation.z);
            float newY = _stackTransform.position.y + _bodyHeight * i;
            var position = _stackTransform.transform.position;
            body.transform.position = new Vector3(position.x, newY, position.z);
        }
    }

    private void Update()
    {
        for (int i = 0; i < _bodies.Count; i++)
        {
            float newY = _stackTransform.position.y + _bodyHeight * i;
            var destiny = new Vector3(_stackTransform.transform.position.x, newY, _stackTransform.transform.position.z);

            _bodies[i].SetTargetPosition(destiny);
            _bodies[i].SetCanFollowTargetPosition(true);

            if (i > 0)
            {
                _bodies[i].SetSpeedMultiplier(i);
            }

        }
    }
}
