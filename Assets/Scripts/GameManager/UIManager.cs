using System.Collections;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private TMP_Text _loadText;
    [SerializeField] private TMP_Text _moneyText;
    
    [Header("References")]
    [SerializeField] private StackBodies _stackBodies;
    [SerializeField] private ShopArea _shopArea;
    [SerializeField] private GameData _gameData;

    private void Start()
    {
        SetInitialValues();
    }

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }
    
    private void SetInitialValues()
    {
        _gameData.CurrentLoad = 0;
        UpdateLoadText();
        UpdateMoneyText();
    }
    
    private void SubscribeToEvents()
    {
        _stackBodies.OnBodyCollect += UpdateLoadText;
        _stackBodies.OnBodySale += UpdateMoneyText;
        _shopArea.OnUpgrade += UpdateLoadText;
        _shopArea.OnMoneyAction += UpdateMoneyText;
    }

    private void UnsubscribeFromEvents()
    {
        _stackBodies.OnBodyCollect -= UpdateLoadText;
        _stackBodies.OnBodySale -= UpdateMoneyText;
        _shopArea.OnUpgrade -= UpdateLoadText;
        _shopArea.OnMoneyAction -= UpdateMoneyText;
    }

    private void UpdateLoadText()
    {
        _loadText.text = _gameData.CurrentLoad.ToString() + "/" + _gameData.LoadLimit.ToString();
        StartCoroutine(TextActionAnimations(_loadText));
    }
    
    private void UpdateMoneyText()
    {
        _moneyText.text = "$" + _gameData.Money.ToString();
        StartCoroutine(TextActionAnimations(_moneyText));
    }

    private IEnumerator TextActionAnimations(TMP_Text text)
    {
        text.fontSize += 7;
        yield return new WaitForSeconds(0.12f);
        text.fontSize -= 7;
    }
}
