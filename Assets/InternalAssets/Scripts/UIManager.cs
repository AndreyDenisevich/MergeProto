using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
public class UIManager : MonoBehaviour
{
    [Inject]
    private GameController _gameController;

    [SerializeField]
    private TextMeshProUGUI _finalText;
    [SerializeField]
    private TextMeshProUGUI _levelText;
    [SerializeField]
    private Text _coinsCount;
    [SerializeField]
    private TextMeshProUGUI _meleeCost;
    [SerializeField]
    private TextMeshProUGUI _rangeCost;
    [SerializeField]
    private Button _buyMeleeButton;
    [SerializeField]
    private Button _buyRangeButton;
    [SerializeField]
    private Button _startFightButton;
    void Start()
    {
        UpdateCoins();
    }
    void Update()
    {

    }

    public void Win()
    {
        _finalText.gameObject.SetActive(true);
        _finalText.text = "You win!";
    }

    public void Lose()
    {
        _finalText.gameObject.SetActive(true);
        _finalText.text = "You lose!";
    }
    public void UpdateCoins()
    {
        _coinsCount.text = _gameController.coins.ToString();
    }

    public void UpdateMeleeCost()
    {
        _meleeCost.text = _gameController.meleeCost.ToString();
    }

    public void UpdateRangeCost()
    {
        _rangeCost.text = _gameController.rangeCost.ToString();
    }

    public void FightStarted()
    {
        _levelText.gameObject.SetActive(false);
        _startFightButton.gameObject.SetActive(false);
        _buyMeleeButton.gameObject.SetActive(false);
        _buyRangeButton.gameObject.SetActive(false);
    }
}
