using System.Collections;
using TMPro;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class ShopArea : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private GameData _gameData;

    [SerializeField] private PlayerTrigger _playerTrigger;

    [Header("UI Settings")] [SerializeField]
    private TMP_Text _text;

    [Header("Upgrade Settings")] [SerializeField]
    private float _moneyActionDelay = 0.4f;

    [SerializeField] private int _defaultUpgradeCost;
    [SerializeField] private Material _playerSkinMaterial;

    private bool _canUpgrade;

    #region Events

    public event Action OnMoneyAction;
    public event Action OnUpgrade;

    #endregion
    
    private void Start()
    {
        UpdateUpgradeCostText();
    }

    private void OnEnable()
    {
        _playerTrigger.OnShopAreaTrigger += CheckIfCanUpgrade;
    }

    private void OnDisable()
    {
        _playerTrigger.OnShopAreaTrigger -= CheckIfCanUpgrade;
    }

    private void CheckIfCanUpgrade(bool canUpgrade)
    {
        if (!canUpgrade)
        {
            _canUpgrade = false;
            return;
        }

        _canUpgrade = true;

        StartCoroutine(GetMoney());
    }

    private IEnumerator GetMoney()
    {
        while (_canUpgrade && _gameData.Money >= 1 && _gameData.UpgradeCost >= 1)
        {
            _gameData.UpgradeCost--;
            _gameData.Money--;

            UpdateUpgradeCostText();

            if (_gameData.UpgradeCost <= 0)
            {
                Upgrade();
                yield break;
            }

            if (_gameData.Money <= 0)
            {
                yield break;
            }


            yield return new WaitForSeconds(_moneyActionDelay);
        }

        yield return null;
    }

    private void UpdateUpgradeCostText()
    {
        _text.text = "$" + _gameData.UpgradeCost.ToString();
        OnMoneyAction?.Invoke();
    }

    private void Upgrade()
    {
        _gameData.LoadLimit += 1;
        OnUpgrade?.Invoke();
        SetNewRandomSkinColor();
        CalculateNewUpgradeCost();
    }

    private void CalculateNewUpgradeCost()
    {
        _gameData.UpgradeCost = Mathf.RoundToInt(_defaultUpgradeCost * 1.5f);

        UpdateUpgradeCostText();
    }

    private void SetNewRandomSkinColor()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);

        _playerSkinMaterial.color = new Color(r, g, b);
    }
}
