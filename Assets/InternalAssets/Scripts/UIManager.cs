using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    [SerializeField]
    private TextMeshProUGUI _finalText;
    [SerializeField]
    private Text _coinsCount;
    [SerializeField]
    private Text _meleeCost;
    [SerializeField]
    private Text _rangeCost;
    [SerializeField]
    private Button _buyMeleeButton;
    [SerializeField]
    private Button _buyRangeButton;
    [SerializeField]
    private Button _startFightButton;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
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
        _coinsCount.text = GameController.instance.coins.ToString();
    }

    public void UpdateMeleeCost()
    {
        _meleeCost.text = GameController.instance.meleeCost.ToString();
    }

    public void UpdateRangeCost()
    {
        _rangeCost.text = GameController.instance.rangeCost.ToString();
    }

    public void FightStarted()
    {
        _startFightButton.gameObject.SetActive(false);
        _buyMeleeButton.gameObject.SetActive(false);
        _buyRangeButton.gameObject.SetActive(false);
    }
}
