using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private StackBodies _stackBodies;
    [SerializeField] private TMP_Text _loadText;
    [SerializeField] private TMP_Text _moneyText;

    private void Start()
    {
        _gameData.CurrentLoad = 0;
        UpdateTexts();
    }

    private void OnEnable()
    {
        _stackBodies.OnBodyAction += UpdateTexts;
    }

    private void OnDisable()
    {
        _stackBodies.OnBodyAction -= UpdateTexts;
    }

    private void UpdateTexts()
    {
        _loadText.text =_gameData.CurrentLoad.ToString() + "/" + _gameData.LoadLimit.ToString();
        _moneyText.text = "$" + _gameData.Money.ToString();
    }
}
