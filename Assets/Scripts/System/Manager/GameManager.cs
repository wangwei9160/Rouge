using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : ManagerBase<GameManager>
{
    public StateID state = StateID.FightState;

    public GameData gameData;   // ÓÎÏ·Êý¾Ý

    private void Start()
    {
        gameData = new GameData();
    }


    void Update()
    {
        if(GameContext.number != null)
        {
            //Debug.Log(GameContext.number.res + " " + GameContext.number.total);
            if(state == StateID.FightState)
            {
                if (GameContext.number.res == GameContext.number.total)
                {
                    TransState(StateID.ShopState);
                }
            }
        }
    }

    public void TransState(StateID newState)
    {
        if(state == newState)
        {
            Debug.Log(string.Format("Current State is {0:D}" ,state));
        }else
        {
            Debug.Log(string.Format("State From {0:D} -> {1:D}", state, newState));
            state = newState;
        }
    }

    public void BuyItemByID(int id)
    {
        Debug.Log(TplUtil.GetItemTplDic()[id].Name);
        ItemFactory.GetItemByID(id).OnGet();
        UIManager.Instance.ShopUI.GetComponent<ShopPanel>().RefreshPlayerAttribute();
    }
}
