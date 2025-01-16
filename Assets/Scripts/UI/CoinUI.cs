using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour 
{
    public Button moreCoin;
    public Text text;

    private void OnEnable()
    {
        EventCenter.AddListener(EventDefine.RefreshPlayerAttribute, OnMoneyChange);
    }

    private void OnDisable()
    {
        EventCenter.RemoveListener(EventDefine.RefreshPlayerAttribute, OnMoneyChange);
    }

    private void Start()
    {
        moreCoin.onClick.AddListener(GetMoney);
    }

    public void GetMoney()
    {
        GameManager.Instance.OnMoneyChange(1000);
    }

    public void OnMoneyChange()
    {
        var m = GameManager.Instance.gameData.money;
        if ( m < 1000000)
        {
            text.text = m.ToString();
        }else 
        {
            text.text = string.Format("{.1f}Íò", m / 1000.0);
        }
        
    }
}