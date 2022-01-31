using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
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
        UpdateCoins();
    }
    void Update()
    {
        
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
