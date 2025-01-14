using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : ManagerBase<GameManager>
{
    public StateID state = StateID.FightState;

    public GameData gameData;   // ��Ϸ����
    public bool isLoadAsset = false;

    private void Start()
    {
        gameData = new GameData();
        gameData.Init();
    }


    void Update()
    {
        if (!isLoadAsset)
        {
            isLoadAsset = true;
            StartCoroutine(LoadAsset());
        }
        if(GameContext.number != null)
        {
            //Debug.Log(GameContext.number.res + " " + GameContext.number.total);
            if(state == StateID.FightState)
            {
                if (GameContext.number.res == GameContext.number.total)
                {
                    TransState(StateID.ShopState);
                    StartCoroutine(Refresh());
                }
            }
        }
    }

    public IEnumerator Refresh()
    {
        UIManager.Instance.ShopUI.GetComponent<ShopPanel>().RefreshAll();
        yield return new WaitForSeconds(0.1f);
    }

    public IEnumerator LoadAsset()
    {
        yield return new WaitForSeconds(0.1f);
        if(AssetManager.Instance != null)
        {
            Debug.Log("Load AssetManager");
        }
        //ItemFactory.GetItemByID(0);
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
        //Debug.Log(TplUtil.GetItemTplDic()[id].Name);
        ItemFactory.GetItemByID(id).OnGet();
        gameData.OnGetItemByID(id);
        UIManager.Instance.ShopUI.GetComponent<ShopPanel>().RefreshPlayerAttribute();
        UIManager.Instance.ShopUI.GetComponent<ShopPanel>().RefreshBagSlot();
    }

    public bool BuyWeaponByID(int id)
    {
        //Debug.Log(TplUtil.GetWeaponTplDic()[id].Name);
        int idx = FindWeaponSlotEmptyIndex();
        if (idx == -1)
        {
            WeaponTplInfo weapon = TplUtil.GetWeaponTplDic()[id];
            if(weapon.Next == -1)
            {
                return false;
            }
            idx = FindWeaponSlotIndexSameAsID(id);
            if(idx == -1)
            {
                return false;
            }else
            {
                gameData.WeaponIDs[idx] = weapon.Next;
            }
        }else
        {
            gameData.WeaponIDs[idx] = id;
        }
        UIManager.Instance.ShopUI.GetComponent<ShopPanel>().RefreshWeaponSlot();
        return true;
    }

    /// <summary>
    /// ���Ժϳ�
    /// </summary>
    /// <param name="id"> ��ƷID </param>
    /// <param name="pos"> ����ֵ </param>
    /// <returns></returns>
    public bool TryMerge(int id , int pos)
    {
        int idx = FindWeaponSlotIndexSameAsID(id, pos);
        if (idx == -1)      // û���ҵ���ͬ������
        {
            return false;
        }
        WeaponTplInfo weapon = TplUtil.GetWeaponTplDic()[id];
        if(weapon.Next == -1) // û����һ���ȼ�������
        {
            return false;
        }
        gameData.WeaponIDs[Math.Min(idx , pos)] = weapon.Next;
        gameData.WeaponIDs[Math.Max(idx , pos)] = -1;
        UIManager.Instance.ShopUI.GetComponent<ShopPanel>().RefreshWeaponSlot();
        return true;
    }

    public void ScaleWeaponByIndex(int idx)
    {
        gameData.WeaponIDs[idx] = -1;
        UIManager.Instance.ShopUI.GetComponent<ShopPanel>().RefreshWeaponSlot();
    }

    // �ҵ�һ����λ
    private int FindWeaponSlotEmptyIndex()
    {
        int idx = -1;
        for (int i = 0; i < gameData.WeaponSlot; i++)
        {
            if (gameData.WeaponIDs[i] == -1)
            {
                idx = i;
                break;
            }
        }
        return idx;
    }

    // �ҵ���һ����id��ͬ��ֵ��λ��
    private int FindWeaponSlotIndexSameAsID(int id)
    {
        int idx = -1;
        for (int i = 0; i < gameData.WeaponSlot; i++)
        {
            if (gameData.WeaponIDs[i] == id)
            {
                idx = i;
                break;
            }
        }
        return idx;
    }

    // �ҵ���һ����������pos��ֵ��id��λ��
    private int FindWeaponSlotIndexSameAsID(int id , int pos)
    {
        int idx = -1;
        for (int i = 0; i < gameData.WeaponSlot; i++)
        {
            if (i == pos) continue;
            if (gameData.WeaponIDs[i] == id)
            {
                idx = i;
                break;
            }
        }
        return idx;
    }
}
