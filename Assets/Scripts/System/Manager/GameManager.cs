using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;


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
                    OnMoneyChange(gameData.playerAttr.Revenues); // ÿ�غϹ̶�����
                    EventCenter.Broadcast(EventDefine.ShowShopUI);
                }
            }
        }
    }

    public IEnumerator LoadAsset()
    {
        yield return new WaitForSeconds(0.1f);
        if (AssetManager.Instance != null)
        {
            Debug.Log("Load AssetManager");
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

    public void OnMoneyChange(int money_)
    {
        if(money_ < 0)
        {
            gameData.Cost -= money_;
        }
        gameData.money += money_;
        EventCenter.Broadcast(EventDefine.RefreshPlayerAttribute);
    }

    private bool TryPayMoney(int money_)
    {
        if(gameData.money < money_)
        {
            return false;
        }
        return true;
    }

    public bool HasItem(int id)
    {
        return gameData.HasItem(id);
    }

    public bool BuyItemByID(int id)
    {
        //Debug.Log(TplUtil.GetItemTplDic()[id].Name);
        
        int needMoney = TplUtil.GetItemTplDic()[id].Price;
        if (!TryPayMoney(needMoney))
        {
            EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI, "��ǰ��Ҳ���");
            return false;
        }
        OnMoneyChange( -1 * needMoney);
        ItemFactory.GetItemByID(id).OnGet();
        gameData.OnGetItemByID(id);
        EventCenter.Broadcast(EventDefine.RefreshItem);
        return true;
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
                EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI, "��ǰ������û�п�λ");
                return false;
            }
            idx = FindWeaponSlotIndexSameAsID(id);
            if(idx == -1)
            {
                EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI, "��ǰ������û�п�λ");
                return false;
            }else
            {
                int needMoney = TplUtil.GetWeaponTplDic()[id].Price;
                if (!TryPayMoney(needMoney))
                {
                    EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI, "��ǰ��Ҳ���");
                    return false;
                }
                gameData.WeaponIDs[idx] = weapon.Next;
            }
        }else
        {
            int needMoney = TplUtil.GetWeaponTplDic()[id].Price;
            if (!TryPayMoney(needMoney))
            {
                EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI, "��ǰ��Ҳ���");
                return false;
            }
            OnMoneyChange( -1 * needMoney);
            gameData.WeaponIDs[idx] = id;
        }
        EventCenter.Broadcast(EventDefine.RefreshWeapon);
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
        WeaponTplInfo weapon = TplUtil.GetWeaponTplDic()[id];
        if (weapon.Next == -1) // û����һ���ȼ�������
        {
            EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI, "��ǰ�����޷���һ������");
            return false;
        }
        int idx = FindWeaponSlotIndexSameAsID(id, pos);
        if (idx == -1)      // û���ҵ���ͬ������
        {
            EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI, "��ǰ��������ͬϡ�жȵ�ͬ�������޷���һ���ϳ�");
            return false;
        }
        gameData.WeaponIDs[Math.Min(idx , pos)] = weapon.Next;
        gameData.WeaponIDs[Math.Max(idx , pos)] = -1;
        EventCenter.Broadcast(EventDefine.RefreshWeapon);
        return true;
    }

    public void ScaleWeaponByIndex(int idx)
    {
        gameData.WeaponIDs[idx] = -1;
        EventCenter.Broadcast(EventDefine.RefreshWeapon);
    }

    /// <summary>
    /// ��������
    /// </summary>

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
