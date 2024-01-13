using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerTrigger _playerTrigger;
    [SerializeField] private GameObject _shopScreen;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        _playerTrigger.OnShopAreaTrigger += OpenShopScreen;
    }

    private void OnDisable()
    {
        _playerTrigger.OnShopAreaTrigger -= OpenShopScreen;
    }

    private void OpenShopScreen(bool opened)
    {
        if (opened)
        {
            _shopScreen.SetActive(true);
            return;
        }
        _shopScreen.SetActive(false);
    }
}
